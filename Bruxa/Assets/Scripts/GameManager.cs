using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public Player player;
    public float clickRange = 0.25f;

    public List<Slot> inventario = new List<Slot>();

    public bool listenInput = true;

    void Awake() {
        instance = this;
    }

    void Update() {
        if (!listenInput) return;

        if (Input.GetMouseButton(0)) {
            Vector3 mousePosition = GetMousePosition();

            if (mousePosition != Vector3.zero) {
                Action action = CheckForClickables(mousePosition);
                player.SetTarget(mousePosition, action);
            }
        }
    }

    Action CheckForClickables(Vector3 clickPos) {
        Collider[] colliders = Physics.OverlapSphere(clickPos, clickRange);
        foreach (Collider collider in colliders) {
            if (collider.GetComponent<IAcao>() != null) {
                return collider.GetComponent<IAcao>().FazerAcao;
            }
        }

        return null;
    }

    Vector3 GetMousePosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            return hit.point;
        }
        return Vector3.zero;
    }

}
