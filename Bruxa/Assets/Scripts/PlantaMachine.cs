using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaMachine : MonoBehaviour {
    public GameObject[] estados;

    public IState sementeState, brotoState, mudaState, crescidoState, mortoState;

    public IState state;

    public IState lastState;
    public bool inHand = true;

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
        return inHand? 0 : 1;
    }
    
    public void ChangeState(IState state){
        this.state?.Exit();
        this.state = state;
        this.state?.Enter();

        Debug.Log("Mudou de estado para " + state.GetType().Name);
    }

    public virtual void HasTimeChangedState(IState stateChanged) {
        if (stateChanged == lastState) return;

        lastState = stateChanged;
    }

    void FixedUpdate() {
        state?.Update();
    }
}
