using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Slot {
    public Signo item;
    public int qtd;
    public Action<Signo,int> OnChange;

    public Slot(Slot slot) {
        item = slot.item;
        qtd = slot.qtd;
    }

    public Slot(Signo item, int qtd) {
        this.item = item;
        this.qtd = qtd;
    }

    public void Add(int qtd) {
        this.qtd += qtd;
        Debug.Log(qtd);
        Debug.Log(OnChange);
        if (OnChange != null) OnChange(item, qtd);
    }

    public void Subtract(int qtd) {
        this.qtd -= qtd;

        if (this.qtd < 0) this.qtd = 0;
 
        if (OnChange != null) OnChange(item, qtd);
    }

    public void SetQuantity(int qtd) {
        this.qtd = qtd;

        if (OnChange != null) OnChange(item, qtd);
    }

    public GameObject GetObj(bool subtract = true) {
        if (qtd == 0) return null;

        GameObject prefab = item.prefab;
        GameObject novoObj = GameObject.Instantiate(prefab);

        if (subtract)  Subtract(1);
        
        return novoObj;
    }

    public bool IsInstantiable() {
        return item.prefab != null;
    }
}
