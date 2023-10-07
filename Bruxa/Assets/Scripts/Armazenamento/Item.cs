using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : AcaoBehaviour {
    public Signo signo;
    public bool pegavel = true;

    public override void FazerAcao() {
        if (!pegavel) return;
        
        if (Player.instance.IsHandEmpty()) {
            Player.instance.PutInHand(gameObject);
        }
    }
}
