using UnityEngine;

public class VersaoController : MonoBehaviour {
    public static VersaoController instance;
    public System.Action<int> TrocaVersao;

    public int versao = 0;

    void Awake() {
        instance = this;

        // Escolhe uma versão aleatoria
        versao = Random.Range(0, 7);
        Mudar(versao);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            versao++;
            Mudar(versao);
        }
    }

    public void Mudar(int versao) {
        this.versao = versao % 7;
        TrocaVersao?.Invoke(this.versao);
    }
}
