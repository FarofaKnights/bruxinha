using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour {
    Slot slot;

    public Image image, fundo;
    public Text txt;
    public bool sumirFundo = false;

    void Start() {
        UpdateText();
        UpdateImage();
    }

    public void HandleClick() {
        if (slot == null) return;
        if (slot.qtd == 0) return;

        if (Player.instance.IsHandEmpty()) {
            Player.instance.PutInHand(slot);
            slot = null;
            UpdateText();
            UpdateImage();
        }
    }

    public void SetSlot(Signo signo, int qtd) {
        slot = new Slot(signo, qtd);
        UpdateText();
        UpdateImage();
    }

    public void SetSlot(Slot slot) {
        this.slot = (slot != null) ? new Slot(slot) : null;
        UpdateText();
        UpdateImage();
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
        UpdateImage();
    }

    public int GetQtd() {
        if (slot == null) return 0;
        return slot.qtd;
    }

    void UpdateText() {
        if (slot == null || slot.qtd == 0) {
            txt.text = "";
            return;
        }

        txt.text = slot.qtd.ToString();
    }

    void UpdateImage() {
        if (slot == null || slot.qtd == 0) {
            image.sprite = null;

            if (sumirFundo) {
                fundo.enabled = false;
            }

            return;
        } else {
            if (sumirFundo) {
                fundo.enabled = true;
            }
        }

        image.sprite = null;
    }
}
