using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotoEstado : MonoBehaviour, IState {
    public PlantaMachine plantaMachine;
    public float timeToGrow;
    public float currentTime;

    public void Enter() {
        currentTime = 0;
        gameObject.SetActive(true);
    }

    public void Update() {
        currentTime += Time.deltaTime * plantaMachine.GetModificador();

        if (currentTime >= timeToGrow) {
            plantaMachine.ChangeState(plantaMachine.mudaState);
        }
        else if (currentTime <= -timeToGrow) {
            plantaMachine.ChangeState(plantaMachine.sementeState);
        }
    }

    public void Exit() {
        gameObject.SetActive(false);
    }
}
