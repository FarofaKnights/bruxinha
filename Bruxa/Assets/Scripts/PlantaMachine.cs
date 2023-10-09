using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Relacao { Neutro, Para, Cresce, Murcha }

public class PlantaMachine : MonoBehaviour {
    public GameObject[] estados;

    public IState sementeState, brotoState, mudaState, crescidoState, mortoState;

    public IState state;

    public Relacao chuva = Relacao.Cresce, neve = Relacao.Para, sol = Relacao.Para, lua = Relacao.Para, mudanca = Relacao.Neutro;

    public TempoState lastState;
    public bool inHand = true;
    public float modificador = 1;

    public float tempoEntreFases = 6f;
    public float tempoMorte = 12f;

    void Awake() {
        sementeState = estados[0].GetComponent<IState>();
        brotoState = estados[1].GetComponent<IState>();
        mudaState = estados[2].GetComponent<IState>();
        crescidoState = estados[3].GetComponent<IState>();
    }

    public void Plantar() {
        ChangeState(sementeState);
        TempoMachine.instance.AddListener(HasTimeChangedState);
        inHand = false;
    }

    public void Pegar() {
        TempoMachine.instance.RemoveListener(HasTimeChangedState);
        inHand = true;

        ChangeItemTo changeItemTo = GetComponent<ChangeItemTo>();
        if (changeItemTo != null) {
            changeItemTo.Change();
        }

        Destroy(gameObject);
    }

    public void Morrer() {
        TempoMachine.instance.RemoveListener(HasTimeChangedState);
        // morrer
    }

    public void Destruir() {
        TempoMachine.instance.RemoveListener(HasTimeChangedState);
        Destroy(gameObject);
    }

    public float GetModificador() {
        return inHand? 0 : modificador;
    }
    
    public void ChangeState(IState state){
        this.state?.Exit();
        this.state = state;
        this.state?.Enter();
    }

    float CalcularRelacao(float modificador, Relacao relacao) {
        if (relacao == Relacao.Neutro) return modificador;
        if (relacao == Relacao.Para) return 0;
        if (relacao == Relacao.Cresce) return modificador + 0.5f;
        if (relacao == Relacao.Murcha) return -0.5f;
        return modificador;
    }

    public virtual void HasTimeChangedState(TempoState stateChanged) {
        if (stateChanged == lastState) return;

        modificador = 0;

        if (stateChanged.sol) {
            modificador = CalcularRelacao(modificador, sol);
        } else if (stateChanged.lua) {
            modificador = CalcularRelacao(modificador, lua);
        }

        if (modificador != 0) modificador = Mathf.Sign(modificador);

        if (mudanca != Relacao.Neutro && mudanca != Relacao.Para) {
            if (lastState != null && lastState.lua && stateChanged.sol) {
                modificador = Mathf.Sign(CalcularRelacao(modificador, mudanca));
                state?.Execute(999);
                modificador = 0;
            }
        }

        if (stateChanged.chuva) {
            modificador = CalcularRelacao(modificador, chuva);
        }

        if (stateChanged.neve) {
            modificador = CalcularRelacao(modificador, neve);
        }

       // Debug.Log(stateChanged.sol + " - " + stateChanged.lua + " - " + stateChanged.chuva + " - " + stateChanged.neve + " - " + mudanca + " - " + modificador);

        lastState = stateChanged;
    }

    void FixedUpdate() {
        state?.Execute(Time.deltaTime);
    }
}
