using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TempoMachine : MonoBehaviour {
    IState state;
    public string debugState;

    Action OnChanceState;

    void Start(){
        ChangeState(new DiaState(this));
    }

    void FixedUpdate(){
        state?.Update();
    }

    public void ChangeState(IState state){
        this.state?.Exit();
        this.state = state;
        this.state?.Enter();

        debugState = state.GetType().Name;

        OnChanceState?.Invoke();
    }

    public void AddListener(Action action){
        OnChanceState += action;
    }

    public void RemoveListener(Action action){
        OnChanceState -= action;
    }
}
