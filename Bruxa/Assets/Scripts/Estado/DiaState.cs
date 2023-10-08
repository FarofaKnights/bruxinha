using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaState : IState {
    TempoMachine tempoMachine;
    public float time, multiplier = 1;

    public DiaState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
    }

    public void Enter() {
        time = 0;

        Color fundo = new Color32(0,181,255,255);
        tempoMachine.ChangeAmbient(Color.white, fundo);
    }

    public void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            tempoMachine.ChangeState(new TardeState(tempoMachine));
        }
    }

    public void Exit() {
        time = 0;
    }
}
