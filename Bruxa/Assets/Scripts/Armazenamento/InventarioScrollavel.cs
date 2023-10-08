using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventarioScrollavel : InventarioLista {
    int selecionadoIndex = 0;
    public Action<Slot> OnCurrentChange;

    public Slot GetAtual() {
        if (slots.Count == 0) return null;

        if (selecionadoIndex >= slots.Count) {
            selecionadoIndex = 0;

            if (OnCurrentChange != null) OnCurrentChange(GetAtual());
        }

        return slots[selecionadoIndex];
    }

    public Slot GetAnterior() {
        if (slots.Count == 0) return null;

        if (selecionadoIndex == 0) {
            return slots[slots.Count - 1];
        } else {
            return slots[selecionadoIndex - 1];
        }
    }

    public Slot GetProximo() {
        if (slots.Count == 0) return null;

        if (selecionadoIndex == slots.Count - 1) {
            return slots[0];
        } else {
            return slots[selecionadoIndex + 1];
        }
    }

    public void SelecionarAnterior() {
        if (selecionadoIndex == 0) {
            selecionadoIndex = slots.Count - 1;
        } else {
            selecionadoIndex--;
        }

        if (OnCurrentChange != null) OnCurrentChange(GetAtual());
    }

    public void SelecionarProximo() {
        if (selecionadoIndex == slots.Count - 1) {
            selecionadoIndex = 0;
        } else {
            selecionadoIndex++;
        }

        if (OnCurrentChange != null) OnCurrentChange(GetAtual());
    }

    public void Selecionar(Signo signo) {
        Slot slot = GetSlot(signo);
        if (slot == null) return;

        selecionadoIndex = slots.IndexOf(slot);

        if (OnCurrentChange != null) OnCurrentChange(GetAtual());
    }

    public GameObject InstantiateFromHand() {
        Slot slot = GetAtual();
        if (slot == null) return null;
        if (!slot.IsInstantiable()) return null;

        GameObject obj = slot.GetObj();

        return obj;
    }

    protected override void OnRemove(Slot slot) {
        if (selecionadoIndex >= slots.Count && selecionadoIndex > 0) {
            selecionadoIndex = 0;
            if (OnCurrentChange != null) OnCurrentChange(GetAtual());
        }
    }
}
