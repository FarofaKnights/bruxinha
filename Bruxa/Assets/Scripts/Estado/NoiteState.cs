using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiteState : IState {
    TempoMachine tempoMachine;
    public float time, multiplier = 1;

    public NoiteState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;

        Color noiteFundo = new Color32(1,0,55,255);
        Color pos = new Color32(117,112,209,255);
        tempoMachine.ChangeAmbient(pos, noiteFundo);
    }

    public void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            tempoMachine.ChangeState(new MadrugadaState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
