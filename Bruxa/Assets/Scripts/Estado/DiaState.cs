using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaState : TempoState {

    public DiaState(TempoMachine tempoMachine) {
        this.tempoMachine = tempoMachine;
        multiplier = 1f;

        sol = true;
    }

    public override void Enter() {
        time = 0;

        Color fundo = new Color32(0,181,255,255);
        tempoMachine.ChangeAmbient(Color.white, fundo);
    }

    public override void Execute(float deltaTime) {
        time += deltaTime;
        if (time >= tempoMachine.tempoChange * multiplier) {
            int chance = Random.Range(0, 10);

            if (chance < tempoMachine.chanceChuva) tempoMachine.ChangeState(new ChuvaState(tempoMachine));
            else tempoMachine.ChangeState(new TardeState(tempoMachine));
        }
    }

    public override void Exit() {
        time = 0;
    }
}
