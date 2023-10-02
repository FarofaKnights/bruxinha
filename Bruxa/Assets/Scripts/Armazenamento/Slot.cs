using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot {
    public Signo item;
    public int qtd;

    public Slot(Slot slot) {
        item = slot.item;
        qtd = slot.qtd;
    }

    public Slot(Signo item, int qtd) {
        this.item = item;
        this.qtd = qtd;
    }

    public void AddQtd(int qtd) {
        this.qtd += qtd;
    }

    public void SubQtd(int qtd) {
        this.qtd -= qtd;

        if (this.qtd < 0) this.qtd = 0;
    }

    public void SetQtd(int qtd) {
        this.qtd = qtd;
    }

    public GameObject GetObj() {
        if (qtd == 0) return null;

        GameObject prefab = item.prefab;
        GameObject novoObj = GameObject.Instantiate(prefab);
        SubQtd(1);
        return novoObj;
    }
}
