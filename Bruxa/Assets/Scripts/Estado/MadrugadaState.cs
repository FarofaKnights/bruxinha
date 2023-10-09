using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadrugadaState : TempoState {

    public MadrugadaState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
        multiplier = 0.75f;

        lua = true;
    }

    public override void Enter() {
        time = 0;

        Color fundo = new Color32(0,14,35,255);
        Color pos = new Color32(76,72,180,255);
        tempoMachine.ChangeAmbient(pos, fundo);
    }

    public override void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            tempoMachine.ChangeState(new DiaState(tempoMachine));
        }
    }

    public override void Exit() {
        time = 0;
    }
}
