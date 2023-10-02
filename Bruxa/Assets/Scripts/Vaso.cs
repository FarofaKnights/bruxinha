using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaso : MonoBehaviour, IAcao {
    public GameObject plantaHolder;
    PlantaMachine planta;

    public void FazerAcao() {
        if (!Player.instance.IsHandEmpty()) {
            GameObject maoPlayer = Player.instance.GetInHand();
            if (maoPlayer.GetComponent<PlantaMachine>() != null) {
                Plantar(maoPlayer.GetComponent<PlantaMachine>());
            }
        } else if (planta != null) {
            if (planta.state == planta.crescidoState) {
                Item item = planta.GetComponent<Item>();
                Signo signo = item.signo;


                planta.Pegar();
                planta = null;
            }
        }
    }

    public bool Plantar(PlantaMachine planta) {
        GameObject obj = planta.gameObject;
        planta.transform.SetParent(plantaHolder.transform);
        planta.transform.localPosition = Vector3.zero;
        planta.Plantar();
        this.planta = planta;
        return true;
    }
}
