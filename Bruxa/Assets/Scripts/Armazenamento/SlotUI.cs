using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour {
    Slot slot;

    public Image image;
    public Text txt;

    void Start() {
        UpdateText();
    }

    public void HandleClick() {
        if (slot == null) return;
        if (slot.qtd == 0) return;

        if (Player.instance.IsHandEmpty()) {
            Player.instance.PutInHand(slot);
            slot = null;
            UpdateText();
        }
    }

    public void SetSlot(Slot slot) {
        this.slot = new Slot(slot);
        UpdateText();
    }

    public Signo GetSigno() {
        if (slot == null) return null;
        if (slot.qtd == 0) return null;
        return slot.item;
    }

    public void AddQtd(int qtd) {
        slot.AddQtd(qtd);
        UpdateText();
    }

    public void SubQtd(int qtd) {
        slot.SubQtd(qtd);

        if (slot.qtd == 0) {
            slot = null;
        }

        UpdateText();
    }

    void UpdateText() {
        if (slot == null || slot.qtd == 0) {
            txt.text = "";
            return;
        }

        txt.text = slot.qtd.ToString();
    }

}
