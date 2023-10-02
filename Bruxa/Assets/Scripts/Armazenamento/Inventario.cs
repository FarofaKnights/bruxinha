using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {
    public SlotUI[] slots;
    public bool deleteEmpty = false;

    public void AddItens(Slot slot) {
        int qtd = slot.qtd;
        Signo signo = slot.item;

        AddQtd(signo, qtd);
    }

    public int GetQtd(Signo signo) {
        foreach (SlotUI slotUI in slots) {
            if (slotUI.GetSigno() == signo) {
                return slotUI.GetQtd();
            }
        }

        return 0;
    }

    public void AddQtd(Signo signo, int qtd) {
        SlotUI slotUI = GetSlot(signo);

        if (slotUI == null) {
            slotUI = GetNextEmpty();
            if (slotUI == null) return;

            slotUI.SetSlot(signo, qtd);
            return;
        } else {
            slotUI.AddQtd(qtd);
        }
    }

    public void SubQtd(Signo signo, int qtd) {
        SlotUI slotUI = GetSlot(signo);
        if (slotUI == null) return;

        slotUI.SubQtd(qtd);

        if (slotUI.GetQtd() == 0 && deleteEmpty) {
            slotUI.SetSlot(null);
        }
    }

    public void SetQtd(Signo signo, int qtd) {
        SlotUI slotUI = GetSlot(signo);
        if (slotUI == null) {
            slotUI = GetNextEmpty();
            if (slotUI == null || qtd == 0 || signo == null) return;

            slotUI.SetSlot(signo, qtd);
            return;
        } else {
            slotUI.SetSlot(signo, qtd);
        }

    }

    public SlotUI GetSlot(Signo signo) {
        foreach (SlotUI slotUI in slots) {
            if (slotUI.GetSigno() == signo) {
                return slotUI;
            }
        }

        return null;
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
