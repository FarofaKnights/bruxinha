using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeveState : TempoState {

    public NeveState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
        multiplier = 1f;

        neve = true;
        lua = true;
    }

    public override void Enter() {
        time = 0;

        Color fundo = new Color32(82,82,82,255);
        Color pos = new Color32(103,103,103,255);
        tempoMachine.ChangeAmbient(pos, fundo);
        tempoMachine.SnowParticle(true);
    }

    public override void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            tempoMachine.ChangeState(new DiaState(tempoMachine));
        }
    }

    public override void Exit() {
        tempoMachine.SnowParticle(false);
        time = 0;
    }
}
