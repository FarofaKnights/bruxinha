using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadrugadaState : IState {
    TempoMachine tempoMachine;
    public float time, multiplier = 0.75f;

    public MadrugadaState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;

        Color fundo = new Color32(0,14,35,255);
        Color pos = new Color32(99,93,213,255);
        tempoMachine.ChangeAmbient(pos, fundo);
    }

    public void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            tempoMachine.ChangeState(new DiaState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
