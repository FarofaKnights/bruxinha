using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudaEstado : MonoBehaviour, IState {
    public PlantaMachine plantaMachine;
    public float timeToGrow;
    public float currentTime;

    public void Enter() {
        currentTime = 0;
        timeToGrow = plantaMachine.tempoEntreFases;
        gameObject.SetActive(true);
    }

    public void Execute(float deltaTime) {
        currentTime += deltaTime * plantaMachine.GetModificador();

        if (currentTime >= timeToGrow) {
            plantaMachine.ChangeState(plantaMachine.crescidoState);
        }
        else if (currentTime <= -timeToGrow) {
            plantaMachine.ChangeState(plantaMachine.brotoState);
        }
    }

    public void Exit() {
        gameObject.SetActive(false);
    }
}
