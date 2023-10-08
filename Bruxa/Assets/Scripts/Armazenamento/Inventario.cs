using System.Collections;
using System.Collections.Generic;

public interface Inventario {
    public bool Add(Signo item, int qtd);
    public bool Subtract(Signo item, int qtd);
    public bool SetQuantity(Signo item, int qtd);
    public int GetQuantity(Signo item);
    public bool Has(Signo item);
    public List<Slot> GetSlots();
    public Slot GetSlot(Signo item);
}
