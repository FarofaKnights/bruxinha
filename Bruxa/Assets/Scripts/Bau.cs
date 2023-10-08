using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : AcaoBehaviour {
    public static Bau instance;

    public GameObject ui, slotsHolder;

    public Inventario inventario;


    void Awake() {
        instance = this;
    }

    void Start() {
        int quant = slotsHolder.transform.childCount;
        inventario = new InventarioEstatico(quant);

        foreach (Transform child in slotsHolder.transform) {
            SlotUI slotUI = child.GetComponent<SlotUI>();
            Slot startingSlot = slotUI.GetSlot();
            inventario.Add(startingSlot.item, startingSlot.qtd);
            slotUI.SetSlot(inventario.GetSlot(startingSlot.item));
        }
    }

    void FixedUpdate() {
        // Check if player is near
        if (Vector3.Distance(Player.instance.transform.position, transform.position) > 1f) {
            FecharBau();
        }
    }
    
    public override void FazerAcao() {
        AbrirBau();
        /*
        if (Player.instance.mao.GetAtual() != null) {
            Slot slot = Player.instance.mao.GetAtual();
            inventario.Add(slot.item, slot.qtd);
            Player.instance.mao.SetQuantity(slot.item, 0);
        }
        */
    }

    public void AbrirBau() {
        ui.SetActive(true);
    }

    public void FecharBau() {
        ui.SetActive(false);
    }
}
