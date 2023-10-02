using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantemVersao : MonoBehaviour {
    public GameObject[] versoes;

    void Start() {
        int versao = VersaoController.instance.versao;
        TrocaVersao(versao);

        VersaoController.instance.TrocaVersao += TrocaVersao;
    }

    void TrocaVersao(int versao) {
        for (int i = 0; i < versoes.Length; i++) {
            if (i == versao) {
                versoes[i].SetActive(true);
            } else {
                versoes[i].SetActive(false);
            }
        }
    }
}
