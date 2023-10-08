using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventarioEstatico : Inventario {
    protected Slot[] slots;
    public Action<Slot> OnChange;

    public InventarioEstatico(int quant) {
        slots = new Slot[quant];
    }
    
    public bool Add(Signo item, int qtd) {
        Slot slot = GetSlot(item);

        if (slot == null) {
            return CreateSlot(item, qtd);
        }

        slot.qtd += qtd;
        CheckEmpty(slot);

        return true;
    }

    public bool Subtract(Signo item, int qtd) {
        Slot slot = GetSlot(item);

        if (slot != null) {
            slot.qtd -= qtd;
            CheckEmpty(slot);
        }

        return true;
    }

    public bool SetQuantity(Signo item, int qtd) {
        Slot slot = GetSlot(item);
        
        if (slot == null) {
            return CreateSlot(item, qtd);
        }

        slot.qtd = qtd;
        CheckEmpty(slot);

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
        List<Slot> list = new List<Slot>();
        foreach (Slot slot in slots) {
            list.Add(slot);
        }
        return list;
    }

    public Slot GetSlot(Signo item) {
        foreach (Slot slot in slots) {
            if (slot != null && slot.item == item) {
                return slot;
            }
        }
        return null;
    }

    public Slot GetSlot(int index) {
        if (index < 0 || index >= slots.Length) return null;
        return slots[index];
    }

    protected int GetSlotIndex(Signo item) {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i] != null && slots[i].item == item) {
                return i;
            }
        }
        return -1;
    }

    protected bool CreateSlot(Signo item, int qtd) {
        if (qtd == 0) return false;
        
        int index = GetEmptyPosition();
        if (index == -1) return false;

        Slot slot = new Slot(item, qtd);
        slot.OnChange += HandleOnChange;
        slots[index] = slot;
        return true;
    }

    protected int GetEmptyPosition() {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i] == null || slots[i].qtd == 0) {
                return i;
            }
        }
        return -1;
    }

    protected void CheckEmpty(Slot slot) {
        if (slot.qtd <= 0) {
            slot.OnChange -= HandleOnChange;
            
            int index = GetSlotIndex(slot.item);
            slots[index] = null;

            if (OnChange != null) OnChange(slot);
        }
    }

    protected void HandleOnChange(Signo signo, int quant) {
        Slot slot = GetSlot(signo);
        if (OnChange != null) OnChange(slot);
        CheckEmpty(slot);
    }
}
