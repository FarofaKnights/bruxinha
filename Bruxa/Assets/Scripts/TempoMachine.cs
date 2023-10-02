using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TempoMachine : MonoBehaviour {
    public static TempoMachine instance;

    IState state;
    public string debugState;

    Action<IState> OnChanceState;

    void Awake(){
        instance = this;
    }

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

        OnChanceState?.Invoke(state);
    }

    public void AddListener(Action<IState> action){
        OnChanceState += action;
    }

    public void RemoveListener(Action<IState> action){
        OnChanceState -= action;
    }
}
