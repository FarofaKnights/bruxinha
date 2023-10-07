using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : AcaoBehaviour {
    public static Bau instance;

    public GameObject ui;
    public Inventario inventario;


    void Awake() {
        instance = this;
    }

    void FixedUpdate() {
        // Check if player is near
        if (Vector3.Distance(Player.instance.transform.position, transform.position) > 1f) {
            FecharBau();
        }
    }
    
    public override void FazerAcao() {
        AbrirBau();
        
        if (!Player.instance.IsHandEmpty()) {
            Slot slot = Player.instance.RemoveFromHand();
            if (slot != null) {
                inventario.AddItens(slot);
            }
        }
    }

    public void AbrirBau() {
        ui.SetActive(true);
    }

    public void FecharBau() {
        ui.SetActive(false);
    }
}
