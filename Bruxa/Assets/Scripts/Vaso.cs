using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaso : AcaoBehaviour {
    public GameObject plantaHolder;
    PlantaMachine planta = null;

    public override void FazerAcao(bool leftClick) {
        if (planta != null) {
            if (planta.state == planta.crescidoState) {
                planta.Pegar();
                Item item = planta.GetComponent<Item>();
                Signo signo = item.signo;
                planta = null;
                
                Player.instance.mao.Add(signo, 1);
                Player.instance.mao.Selecionar(signo);
            }
        } else if (Player.instance.mao.GetAtual() != null) {
            Slot slot = Player.instance.mao.GetAtual();

            if (slot == null || !slot.IsInstantiable()) return;

            GameObject objNaMao = slot.GetObj(false);

            if (objNaMao.GetComponent<PlantaMachine>() != null) {
                Player.instance.mao.Subtract(slot.item, 1);
                Plantar(objNaMao.GetComponent<PlantaMachine>());
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
