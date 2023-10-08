using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour {
    [SerializeField]
    Slot slot;

    public Image image, fundo;
    public Text txt;
    public bool sumirFundo = false;
    public bool desprender = true;

    void Start() {
        UpdateValues();
    }

    public void HandleClick() {
        if (slot == null) return;
        if (slot.qtd == 0) return;

        Player.instance.mao.Add(slot.item, 1);
        slot.Subtract(1);
        Player.instance.mao.Selecionar(slot.item);
        
        UpdateValues();
    }

    public void SetSlot(Slot slot) {
        this.slot = (slot != null) ? slot : null;

        slot.OnChange += UpdateValues;

        UpdateValues();
    }

    public Signo GetSigno() {
        if (slot == null) return null;
        if (slot.qtd == 0) return null;
        return slot.item;
    }

    public Slot GetSlot() {
        return slot;
    }

    public void Add(int qtd) {
        slot.Add(qtd);
        UpdateText();
    }

    public void Subtract(int qtd) {
        slot.Subtract(qtd);

        if (slot.qtd == 0 && desprender) {
            slot = null;
        }

        UpdateValues();
    }

    public int GetQtd() {
        if (slot == null) return 0;
        return slot.qtd;
    }

    void UpdateValues(Signo sig, int quant) {
        UpdateValues();
    }

    void UpdateValues() {
        UpdateText();
        UpdateImage();
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
            image.gameObject.SetActive(false);

            if (sumirFundo) {
                fundo.enabled = false;
            }
            
            return;
        } else {
            if (sumirFundo) {
                fundo.enabled = true;
            }
        }
        image.gameObject.SetActive(true);
        image.sprite = slot.item.sprite;
    }

    void OnDestroy() {
        if (slot != null) {
            slot.OnChange -= UpdateValues;
        }
    }
}
