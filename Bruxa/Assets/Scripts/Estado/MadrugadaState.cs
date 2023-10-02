using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadrugadaState : IState {
    TempoMachine tempoMachine;
    public float time, changeTime;

    public MadrugadaState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;
        changeTime = 5;
        tempoMachine.luz.color = Color.black;
    }

    public void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= changeTime) {
            tempoMachine.ChangeState(new DiaState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
