using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaso : MonoBehaviour, IAcao {
    public GameObject plantaHolder;
    public void FazerAcao() {
        Debug.Log("Vaso");
        if (!Player.instance.IsHandEmpty()) {
            Debug.Log("Não vazio");
            GameObject maoPlayer = Player.instance.GetInHand();
            if (maoPlayer.GetComponent<PlantaMachine>() != null) {
                Debug.Log("Planta");
                Plantar(maoPlayer.GetComponent<PlantaMachine>());
            }
        }
    }

    public bool Plantar(PlantaMachine planta) {
        GameObject obj = planta.gameObject;
        planta.transform.SetParent(plantaHolder.transform);
        planta.transform.localPosition = Vector3.zero;
        return true;
    }
}
