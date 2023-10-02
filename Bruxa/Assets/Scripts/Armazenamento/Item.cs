using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IAcao {
    public Signo signo;
    public bool pegavel = true;

    public void FazerAcao() {
        if (!pegavel) return;
        
        if (Player.instance.IsHandEmpty()) {
            Player.instance.PutInHand(gameObject);
        }
    }
}
