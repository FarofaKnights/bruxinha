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

    Slot slotMao;

    Action AfterArrive;
    bool walking = false;

    void Awake() {
        instance = this;
    }

    void FixedUpdate() {
        if (CheckArrivedAtTarget()) {
            AfterArrive?.Invoke();
            AfterArrive = null;
        }
    }

    public void SetTarget(Vector3 target, AcaoBehaviour acao = null) {
        if (acao != null) {
            Transform novoTarget = acao.GetTarget(transform);
            target = novoTarget!=null? novoTarget.position : target;
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

    public void PutInHand(GameObject obj) {
        // Checa se objeto é um item valido
        Item item = obj.GetComponent<Item>();
        if (item == null) return;
        Signo signo = item.signo;
        if (signo == null) return;

        // Se não tiver nada na mão, coloca o item na mão, caso contrário, se o item for o mesmo da mão, adiciona 1 na quantidade
        if (slotMao == null) {
            slotMao = new Slot(signo, 1);
            obj.transform.SetParent(maoPlayer.transform);
            obj.transform.localPosition = Vector3.zero;
        } else if (slotMao.item == signo) {
            slotMao.AddQtd(1);
        }

        Debug.Log("+ " + slotMao.item + " -> " + slotMao.qtd);
    }
    
    public void PutInHand(Slot slot) {
        if (slotMao == null) {
            slotMao = slot;
            GameObject obj = slotMao.GetObj(); // meramente visual
            slot.AddQtd(1);
            
            obj.transform.SetParent(maoPlayer.transform);
            obj.transform.localPosition = Vector3.zero;
        } else if (slotMao.item == slot.item) {
            slotMao.AddQtd(slot.qtd);
        }

        Debug.Log("+ " + slotMao.item + " -> " + slotMao.qtd);
    }

    public GameObject GetInHand(){
        if (slotMao == null) return null;

        GameObject obj = slotMao.GetObj();

        Debug.Log("- " + slotMao.item + " -> " + slotMao.qtd);

        if (slotMao.qtd == 0) {
            Destroy(maoPlayer.transform.GetChild(0).gameObject);
            slotMao = null;
        }

        return obj;
    }

    public Slot RemoveFromHand() {
        if (slotMao == null) return null;

        Destroy(maoPlayer.transform.GetChild(0).gameObject);

        Slot slot = slotMao;
        slotMao = null;

        return slot;
    }

    public GameObject LookInHand() {
        if (slotMao == null) return null;

        return slotMao.item.prefab;
    }

    public bool IsHandEmpty() {
        return slotMao == null;
    }
}
