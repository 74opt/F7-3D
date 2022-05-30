using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Rigidbody rigidbody;
    private Transform transform;
    private Collider collider;
    private float velocity = .25f;
    private float health = 20;
    private float damage = 10;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        collider = GetComponent<Collider>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, 2f, 0), velocity);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Projectile") {
            health -= other.gameObject.GetComponent<Bullet>().damage;
        }

        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
