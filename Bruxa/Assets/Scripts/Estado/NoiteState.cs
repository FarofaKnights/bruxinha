using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiteState : IState {
    TempoMachine tempoMachine;
    public float time, changeTime;

    public NoiteState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;
        changeTime = 5;
        tempoMachine.luz.color = Color.blue;
    }

    public void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= changeTime) {
            tempoMachine.ChangeState(new MadrugadaState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
