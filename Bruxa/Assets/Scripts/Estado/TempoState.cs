using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TempoState : IState {
    public bool sol = false, lua = false, chuva = false, neve = false;
    public float time, multiplier = 1f;
    protected TempoMachine tempoMachine;

    public abstract void Enter();
    public abstract void Execute(float deltaTime);
    public abstract void Exit();
}
