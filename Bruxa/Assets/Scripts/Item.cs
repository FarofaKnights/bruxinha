using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : AcaoBehaviour {
    public Signo signo;
    public bool pegavel = true;

    public override void FazerAcao() {
        if (!pegavel) return;
        
        if (signo != null) {
            Player.instance.mao.Add(signo, 1);
            Destroy(gameObject);
        } else {
            Debug.Log("Item sem signo");
        }
    }
}
