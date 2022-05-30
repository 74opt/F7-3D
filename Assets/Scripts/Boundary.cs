using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {
    private Collider collider;

    void Awake() {
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Boundary") {
            Physics.IgnoreCollision(other.collider, collider);
        }
    }
}
