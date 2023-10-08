using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Player : MonoBehaviour {
    public static Player instance;

    public float arriveDistance = 0.25f;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject maoPlayer;
    Signo signoNaMao = null;

    public InventarioScrollavel mao = new InventarioScrollavel();

    Action<bool> AfterArrive;
    bool walking = false;
    bool wasLeftClick = true;

    void Awake() {
        instance = this;
    }

    void Start() {
        mao.OnChange += UpdateHand;
        mao.OnCurrentChange += UpdateHand;
        UpdateHand(mao.GetAtual());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            mao.SelecionarProximo();
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            mao.SelecionarAnterior();
        }
    }

    void FixedUpdate() {
        if (CheckArrivedAtTarget()) {
            if (AfterArrive != null) AfterArrive(wasLeftClick);
            AfterArrive = null;
        }
    }

    public void SetTarget(Vector3 target, AcaoBehaviour acao = null, bool leftClick = true) {
        wasLeftClick = leftClick;

        if (acao != null) {
            Transform novoTarget = acao.GetTarget(transform);
            target = novoTarget!=null? novoTarget.position : target;
        }

        if (ignoreClick) {
            ignoreClick = false;
            return;
        }

        agent.SetDestination(target);
        walking = true;

        if (acao != null) {
            AfterArrive = acao.FazerAcao;
        }
    }

    public bool CheckArrivedAtTarget(){
        if (!walking) return false;

        if (!agent.pathPending) {
            if (agent.remainingDistance <= arriveDistance) {
                walking = false;
                return true;
            }
        }

        return false;
    }

    bool ignoreClick = false;
    public void IgnoreCurrentClick() {
        ignoreClick = true;
    }

    public void UpdateHand(Slot atual) {
        Debug.Log(atual);

        UIController.instance.UpdateCurrentSelected(atual);

        if (atual == null || atual.qtd == 0) {
            maoPlayer.SetActive(false);
            return;
        }

        maoPlayer.SetActive(true);
        Signo signo = atual.item;
        
        // if (signoNaMao == signo) return;

        signoNaMao = signo;

        if (maoPlayer.transform.childCount > 0) {
            foreach (Transform child in maoPlayer.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }

        if (signo.prefab != null) {
            GameObject obj = atual.GetObj(false);
            obj.transform.SetParent(maoPlayer.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }
}
