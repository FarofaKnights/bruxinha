using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiteState : TempoState {

    public NoiteState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
        multiplier = 1f;

        lua = true;
    }

    public override void Enter() {
        time = 0;

        Color noiteFundo = new Color32(1,0,55,255);
        Color pos = new Color32(117,112,209,255);
        tempoMachine.ChangeAmbient(pos, noiteFundo);
    }

    public override void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            int chance = Random.Range(0, 10);

            if (chance < tempoMachine.chanceNeve) tempoMachine.ChangeState(new NeveState(tempoMachine));
            else tempoMachine.ChangeState(new MadrugadaState(tempoMachine));
        }
    }

    public override void Exit() {
        time = 0;
    }
}
