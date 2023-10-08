using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TempoMachine : MonoBehaviour {
    public static TempoMachine instance;

    IState state;
    public string debugState;
    public Light luz;

    Action<IState> OnChanceState;

    void Awake(){
        instance = this;
    }

    void Start(){
        ChangeState(new DiaState(this));
    }

    void FixedUpdate(){
        state?.Execute(Time.deltaTime);
    }

    public void ChangeState(IState state){
        this.state?.Exit();
        this.state = state;
        this.state?.Enter();

        debugState = state.GetType().Name;
        // Debug.Log(debugState);

        OnChanceState?.Invoke(state);
    }

    public void AddListener(Action<IState> action){
        OnChanceState += action;
    }

    public void RemoveListener(Action<IState> action){
        OnChanceState -= action;
    }
}
