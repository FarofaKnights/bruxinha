using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SementeEstado : MonoBehaviour, IState {
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
            plantaMachine.ChangeState(plantaMachine.brotoState);
        }
        else if (currentTime <= -timeToGrow) {
            plantaMachine.Destruir();
        }
    }

    public void Exit() {
        gameObject.SetActive(false);
    }
}
