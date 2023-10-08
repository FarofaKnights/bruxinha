using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreClick : AcaoBehaviour {
    public Signo signo;
    public bool pegavel = true;

    public override void FazerAcao(bool leftClick) { }
    public override Transform GetTarget() {
        Player.instance.IgnoreCurrentClick();
        return null;
    }

    public override Transform GetTarget(Transform transform) {
        Player.instance.IgnoreCurrentClick();
        return null;
    }
}
