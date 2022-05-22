using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be extended by other projectiles? Perhaps
public class Bullet : MonoBehaviour {
    protected float timeUntilDelete = 2f;
    protected float velocity = .5f;
    private Vector3 finalPos;
    void Awake() {
        StartCoroutine(Delete());
        finalPos = Gun.finalPos;
        print("bulletPos: " + transform.position);
    }

    // Update is called once per frame
    void Update() {
        // TODO: bullets get stuck in walls
        // if (transform.velocity == new Vector3(0, 0, 0)) {
        //     Destroy(gameObject);
        // }
    }

    void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }

    void FixedUpdate() {
        //transform.Translate(new Vector3(0, .5f, 0), Space.Self);
        transform.position = Vector3.MoveTowards(transform.position, finalPos, velocity);
    }

    IEnumerator Delete() {
        yield return new WaitForSeconds(timeUntilDelete);
        Destroy(gameObject);
    }
}
