using UnityEngine;
using System;

public class VersaoController : MonoBehaviour {
    public static VersaoController instance;
    public Action<int> TrocaVersao;

    public int versao = 0;
    int oldVersao = 0;

    void Awake() {
        instance = this;
    }

    void FixedUpdate() {
        if (versao != oldVersao) {
            oldVersao = versao;
            TrocaVersao?.Invoke(versao);
        }
    }
}
