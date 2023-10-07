using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AcaoBehaviour : MonoBehaviour {
    public Transform[] targets;
    public abstract void FazerAcao();

    public Transform GetTarget() {
        if (targets == null || targets.Length == 0) return null;
        return targets[0];
    }

    public Transform GetTarget(Transform closestTo) {
        if (targets == null || targets.Length == 0) return null;

        float menorDistancia = 999;
        Transform maisPerto = null;

        foreach (Transform target in targets) {
            float distancia = Vector3.Distance(target.position, closestTo.position);
            if (distancia < menorDistancia) {
                menorDistancia = distancia;
                maisPerto = target;
            }
        }

        return maisPerto;
    }
}
