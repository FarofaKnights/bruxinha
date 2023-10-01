using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaState : IState {
    TempoMachine tempoMachine;
    public float time, changeTime;

    public DiaState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;
        changeTime = 5;
    }

    public void Update() {
        time += Time.deltaTime;
        if (time >= changeTime) {
            tempoMachine.ChangeState(new TardeState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
