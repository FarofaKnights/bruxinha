using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuvaState : TempoState {

    public ChuvaState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
        multiplier = 1f;

        chuva = true;
        sol = true;
    }

    public override void Enter() {
        time = 0;

        Color fundo = new Color32(94,126,140,255);
        Color pos = new Color32(198,214,255,255);
        tempoMachine.ChangeAmbient(pos, fundo);
        tempoMachine.RainParticle(true);
    }

    public override void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            tempoMachine.ChangeState(new NoiteState(tempoMachine));
        }
    }

    public override void Exit() {
        tempoMachine.RainParticle(false);
        time = 0;
    }
}
