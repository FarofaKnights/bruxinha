using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Player : MonoBehaviour {
    public static Player instance;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject maoPlayer;

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

    public void SetTarget(Vector3 target, Action callback) {
        agent.SetDestination(target);
        walking = true;

        if (callback != null) {
            AfterArrive += callback;
        }
    }

    public bool CheckArrivedAtTarget(){
        if (!walking) return false;

        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    walking = false;
                    return true;
                }
            }
        }

        return false;
    }

    public void PutInHand(GameObject obj) {
        obj.transform.SetParent(maoPlayer.transform);
        obj.transform.localPosition = Vector3.zero;
    }

    public GameObject GetInHand(){
        if (maoPlayer.transform.childCount == 0) return null;
        return maoPlayer.transform.GetChild(0).gameObject;
    }
}
