using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaMachine : MonoBehaviour, IAcao {
    public GameObject[] estados;
    public GameObject estadoAtual;

    public float time, readyTime, pastTime;

    public void FazerAcao() {
        if (Player.instance.GetInHand() == null) {
            Player.instance.PutInHand(gameObject);
        }
    }
}
