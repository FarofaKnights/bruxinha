using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;

    public Image currentSelected;
    public Text currentSelectedText;

    void Awake() {
        instance = this;
    }

    public void UpdateCurrentSelected(Slot slot) {

        if (slot == null || slot.qtd == 0) {
            currentSelected.gameObject.SetActive(false);
            currentSelectedText.text = "";
            return;
        }

        currentSelected.gameObject.SetActive(true);
        currentSelected.sprite = slot.item.sprite;
        currentSelectedText.text = slot.qtd.ToString();
    }
}
