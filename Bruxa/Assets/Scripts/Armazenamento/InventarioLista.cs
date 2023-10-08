using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventarioLista : Inventario {
    [SerializeField]
    protected List<Slot> slots = new List<Slot>();
    public Action<Slot> OnChange;

    public bool Add(Signo item, int qtd) {
        Slot slot = GetSlot(item);
        if (slot == null) {
            CreateSlot(item, qtd);
        } else {
            slot.Add(qtd);
            CheckEmpty(slot);
        }

        return true;
    }

    public bool Subtract(Signo item, int qtd) {
        Slot slot = GetSlot(item);
        if (slot != null) {
            slot.Subtract(qtd);
            CheckEmpty(slot);
        }

        return true;
    }

    public bool SetQuantity(Signo item, int qtd) {
        Slot slot = GetSlot(item);
        if (slot == null) {
            CreateSlot(item, qtd);
        } else {
            slot.SetQuantity(qtd);
            CheckEmpty(slot);
        }

        return true;
    }

    public int GetQuantity(Signo item) {
        Slot slot = GetSlot(item);
        if (slot == null) {
            return 0;
        } else {
            return slot.qtd;
        }
    }

    public bool Has(Signo item) {
        return GetSlot(item) != null;
    }

    public List<Slot> GetSlots() {
        return slots;
    }

    public Slot GetSlot(Signo item) {
        foreach (Slot slot in slots) {
            if (slot.item == item) {
                return slot;
            }
        }
        return null;
    }
    
    protected void CreateSlot(Signo item, int qtd) {
        Slot slot = new Slot(item, qtd);
        slot.OnChange += HandleOnChange;
        slots.Add(slot);
    }

    protected void CheckEmpty(Slot slot) {
        if (slot.qtd <= 0) {
            slot.OnChange -= HandleOnChange;
            
            slots.Remove(slot);
            OnRemove(slot);
        }
    }

    public void Clear() {
        foreach (Slot slot in slots) {
            slot.OnChange -= HandleOnChange;
            OnRemove(slot);
        }

        slots.Clear();
    }

    protected void HandleOnChange(Signo signo, int quant) {
        Slot slot = GetSlot(signo);
        if (OnChange != null) OnChange(slot);
        CheckEmpty(slot);
    }
    protected virtual void OnRemove(Slot slot) { }

}
