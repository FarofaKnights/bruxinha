using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TardeState : IState {
    TempoMachine tempoMachine;
    public float time, changeTime;

    public TardeState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;
        changeTime = 5;
    }

    public void Update() {
        time += Time.deltaTime;
        if (time >= changeTime) {
            tempoMachine.ChangeState(new NoiteState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
