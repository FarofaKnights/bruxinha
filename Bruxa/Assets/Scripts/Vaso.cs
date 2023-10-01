using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaso : MonoBehaviour, IAcao {
    public GameObject plantaHolder;
    public void FazerAcao() {
        if (Player.instance.GetInHand() != null) {
            GameObject maoPlayer = Player.instance.GetInHand();
            if (maoPlayer.GetComponent<PlantaMachine>() != null) {
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
