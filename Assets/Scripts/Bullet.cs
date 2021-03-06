using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be extended by other projectiles? Perhaps
public class Bullet : MonoBehaviour {
    protected Rigidbody rigidbody;
    protected float timeUntilDelete = 2f;
    protected float velocity = .5f;
    public GameObject explosion;
    private Vector3 finalPos;
    public float damage = 5;

    void Awake() {
        StartCoroutine(Delete());
        finalPos = Gun.finalPos;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        // TODO: bullets get stuck in walls (MUST BE FIXED FOR THOSE COOL EXPLODING BULLETS)
        //if (rigidbody.velocity == new Vector3(0, 0, 0)) {
        //    print("destroyed");
        //    Destroy(gameObject);
        //}
    }

    void OnCollisionEnter(Collision other) {
        print("destroyed by weird");
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void FixedUpdate() {
        //transform.Translate(new Vector3(0, .5f, 0), Space.Self);
        if (finalPos == new Vector3(0, 0, 0)) {
            transform.Translate(new Vector3(0, velocity, 0), Space.Self);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, velocity);
        }
    }

    IEnumerator Delete() {
        yield return new WaitForSeconds(timeUntilDelete);
        Destroy(gameObject);
    }
}
