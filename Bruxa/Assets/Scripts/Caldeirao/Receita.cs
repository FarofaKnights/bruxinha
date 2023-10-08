using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Receita", menuName = "Receita")]
public class Receita : ScriptableObject {
    public List<Slot> slots;
    public Signo resultado;

    public bool ChecarReceita(List<Slot> testSlots) {
        if (testSlots.Count != slots.Count) return false;

        if (!CheckSimilarity(testSlots, slots)) return false;
        if (!CheckSimilarity(slots, testSlots)) return false;

        return true;
    }

    bool CheckSimilarity(List<Slot> slots1, List<Slot> slots2) {
        foreach (Slot slot in slots1) {
            bool found = false;
            foreach (Slot slot2 in slots2) {
                if (slot.item == slot2.item && slot.qtd == slot2.qtd) {
                    found = true;
                    break;
                }
            }
            if (!found) return false;
        }

        return true;
    }
}
