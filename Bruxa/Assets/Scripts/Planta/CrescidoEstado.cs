using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrescidoEstado : MonoBehaviour, IState {
    public PlantaMachine plantaMachine;
    public float timeToGrow, timeToDie;
    public float currentTime;

    public void Enter() {
        currentTime = 0;
        timeToGrow = plantaMachine.tempoEntreFases;
        gameObject.SetActive(true);
    }

    public void Execute(float deltaTime) {
        currentTime += deltaTime * plantaMachine.GetModificador();

        if (currentTime >= timeToDie) {
            plantaMachine.Morrer();
        }
        else if (currentTime <= -timeToGrow) {
            plantaMachine.ChangeState(plantaMachine.mudaState);
        }
    }

    public void Exit() {
        gameObject.SetActive(false);
    }
}
