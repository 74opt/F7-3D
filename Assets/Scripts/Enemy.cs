using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private static Rigidbody rigidbody;
    private static Transform transform;
    private static Collider collider;
    private static float velocity = .05f;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, velocity);
    }
}
