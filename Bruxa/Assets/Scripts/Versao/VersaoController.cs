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

    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            versao++;
            versao = versao % 7;
            TrocaVersao?.Invoke(versao);
            Debug.Log("apertei");
        }
    }
}
