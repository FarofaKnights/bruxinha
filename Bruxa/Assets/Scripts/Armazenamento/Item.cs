using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IAcao {
    public Signo signo;

    public void FazerAcao() {
        if (Player.instance.IsHandEmpty()) {
            Player.instance.PutInHand(gameObject);
        }
    }
}
