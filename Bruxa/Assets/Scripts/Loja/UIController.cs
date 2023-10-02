using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;
    public GameObject painel;
    public Inventario inventario, adicoes;

    void Awake() {
        instance = this;
    }

    public void Abrir() {
        painel.SetActive(true);

        foreach (SlotUI slotUI in inventario.slots) {
            slotUI.SetSlot(null);
        }

        foreach (SlotUI slotUI in adicoes.slots) {
            slotUI.SetSlot(null);
        }

        foreach (Slot slot in GameManager.instance.inventario) {
            inventario.AddItens(slot);
        }
    }

    public void Fechar() {
        painel.SetActive(false);

        // Define valor do inventário do player como o mesmo valor do inventário do UI
        foreach (SlotUI slotUI in inventario.slots) {
            Signo signo = slotUI.GetSigno();
            int qtd = slotUI.GetQtd();

            Slot slot = null;

            foreach (Slot s in GameManager.instance.inventario) {
                if (s.item == signo) {
                    slot = s;
                    break;
                }
            }

            if (slot != null) {
                slot.SetQtd(qtd);
            } else {
                slot = new Slot(signo, qtd);
                GameManager.instance.inventario.Add(slot);
            }
        }

        Inventario inv = Bau.instance.inventario;
        // Adiciona as adições da UI no bau
        foreach (SlotUI slotUI in adicoes.slots) {
            Signo signo = slotUI.GetSigno();
            int qtd = slotUI.GetQtd();

            if (qtd > 0) inv.AddQtd(signo, qtd);
        }

        Ritual.instance.FinishRitual();
    }
}
