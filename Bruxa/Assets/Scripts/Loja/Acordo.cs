using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Acordo : MonoBehaviour {
    public Slot[] requisitos;
    public Slot[] recompensas;

    public GameObject requisitosContainer, recompensasContainer;
    public GameObject slotPrefab, slotFeitoPrefab;

    void Start() {
        for (int i = 0; i < requisitos.Length; i++) {
            Slot slot = requisitos[i];
            GameObject novoSlot = BuildVisualSlot(slot, requisitosContainer);
        }

        for (int i = 0; i < recompensas.Length; i++) {
            Slot slot = recompensas[i];
            GameObject novoSlot = BuildVisualSlot(slot, recompensasContainer);
        }
    }

    bool PossuiRequisitos() {
        Inventario inv = UIController.instance.inventario;

        foreach (Slot slot in requisitos) {
            int qtd = inv.GetQtd(slot.item);
            if (qtd < slot.qtd) return false;
        }

        return true;
    }

    public void HandleAceito() {
        if (!PossuiRequisitos()) return;

        Inventario inv = UIController.instance.inventario;
        Inventario add = UIController.instance.adicoes;
        
        foreach (Slot slot in requisitos) {
            inv.SubQtd(slot.item, slot.qtd);
        }

        foreach (Slot slot in recompensas) {
            add.AddQtd(slot.item, slot.qtd);
        }

        Debug.Log("Aceito");
    }

    GameObject BuildVisualSlot(Slot slot, GameObject parent = null) {
        GameObject novoSlot = Instantiate(slotPrefab);

        if (parent != null) {
            novoSlot.transform.SetParent(parent.transform);
            novoSlot.transform.localPosition = Vector3.zero;
            novoSlot.transform.localScale = Vector3.one;
        }
        
        Image img = novoSlot.GetComponentInChildren<Image>();
        Text txt = novoSlot.GetComponentInChildren<Text>();
        txt.text = slot.qtd.ToString();

        return novoSlot;
    }
    
}
