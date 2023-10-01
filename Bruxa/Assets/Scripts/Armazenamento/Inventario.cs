using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {
    public SlotUI[] slots;

    public void AddItens(Slot slot) {
        SlotUI slotToAdd = null;

        foreach (SlotUI slotUI in slots) {
            if (slotUI.GetSigno() == slot.item) {
                slotToAdd = slotUI;
                break;
            }
        }

        if (slotToAdd == null) {
            slotToAdd = GetNextEmpty();
            if (slotToAdd == null) return;

            slotToAdd.SetSlot(slot);
        } else {
            int qtd = slot.qtd;
            slotToAdd.AddQtd(qtd);
        }
    }

    public SlotUI GetNextEmpty() {
        foreach (SlotUI slotUI in slots) {
            if (slotUI.GetSigno() == null) {
                return slotUI;
            }
        }

        return null;
    }
}
