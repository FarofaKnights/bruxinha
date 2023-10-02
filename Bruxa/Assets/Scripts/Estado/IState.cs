using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState {
    public void Enter();
    public void Execute(float time);
    public void Exit();
}
