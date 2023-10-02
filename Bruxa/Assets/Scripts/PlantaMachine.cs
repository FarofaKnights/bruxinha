using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CresceCom { Sol, Lua, Mudanca }

public class PlantaMachine : MonoBehaviour {
    public GameObject[] estados;

    public IState sementeState, brotoState, mudaState, crescidoState, mortoState;

    public IState state;

    public CresceCom cresceCom;

    public IState lastState;
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

        Debug.Log("Mudou de estado para " + state.GetType().Name);
    }

    public virtual void HasTimeChangedState(IState stateChanged) {
        if (stateChanged == lastState) return;
        bool sol = false, lua = false, mudanca = false;

        switch (stateChanged) {
            case MadrugadaState:
                lua = true;
                break;
            case DiaState:
                sol = true;
                break;
            case TardeState:
                sol = true;
                break;
            case NoiteState:
                lua = true;
                break;
        }

        if (cresceCom == CresceCom.Sol) {
            modificador = sol ? 1 : 0;
        }

        if (cresceCom == CresceCom.Lua) {
            modificador = lua ? 1 : 0;
        }

        if (cresceCom == CresceCom.Mudanca) {
            if (lastState is MadrugadaState && stateChanged is DiaState) {
                modificador = 1;
                state?.Execute(999);
                modificador = 0;
            }
        }

        lastState = stateChanged;
    }

    void FixedUpdate() {
        state?.Execute(Time.deltaTime);
    }
}
