using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TardeState : IState {
    TempoMachine tempoMachine;
    public float time, multiplier = 0.75f;

    public TardeState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;

        Color fundo = new Color32(0,150,255,255);
        Color pos = new Color32(255,170,172,255);
        tempoMachine.ChangeAmbient(pos, fundo);
    }

    public void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            tempoMachine.ChangeState(new NoiteState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
