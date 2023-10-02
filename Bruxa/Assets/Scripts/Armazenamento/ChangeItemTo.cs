using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItemTo : MonoBehaviour {
    public Signo signo;

    public void Change() {
        Item item = GetComponent<Item>();
        item.signo = signo;

        Destroy(this);
    }
}
