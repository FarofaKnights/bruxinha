using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ritual : AcaoBehaviour {
    public static Ritual instance;
    public GameObject center;

    public float liftSpeed = 0.1f, liftLimit = 0.25f;
    public bool beingLifted = false, beingLowered = false;
    Vector3 startPos;

    void Awake() {
        instance = this;
    }
    
    public override void FazerAcao() {
        GameManager.instance.listenInput = false;

        GameObject player = Player.instance.gameObject;
        player.GetComponent<NavMeshAgent>().enabled = false;
        beingLifted = true;
        startPos = player.transform.position;
        Debug.Log("Arrived at center");
    }

    public void InRitual() {
        beingLifted = false;
        // UIController.instance.Abrir();
    }

    public void FinishRitual() {
        beingLowered = true;
    }

    public void FreePuppet() {
        GameObject player = Player.instance.gameObject;
        
        beingLowered = false;
        player.GetComponent<NavMeshAgent>().enabled = true;
        GameManager.instance.listenInput = true;
    }

    void FixedUpdate() {
        if (beingLifted) {
            Debug.Log("Lifting");
            GameObject player = Player.instance.gameObject;
            Vector3 newPos = player.transform.position + Vector3.up * liftSpeed * Time.deltaTime;
            player.transform.position = newPos;

            if (newPos.y >= startPos.y + liftLimit) {
                InRitual();
            }
        }

        if (beingLowered) {
            Debug.Log("Lowering");
            GameObject player = Player.instance.gameObject;
            Vector3 newPos = player.transform.position - Vector3.up * liftSpeed * Time.deltaTime;
            player.transform.position = newPos;

            if (newPos.y <= startPos.y) {
                FreePuppet();
            }
        }
    }
}
