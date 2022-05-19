using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be extended by other projectiles? Perhaps
public class Bullet : MonoBehaviour {
    protected float timeUntilDelete = 2f;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }

    void FixedUpdate() {
        transform.Translate(new Vector3(0, .5f, 0), Space.Self);
    }

    IEnumerator Delete() {
        yield return new WaitForSeconds(timeUntilDelete);
        Destroy(gameObject);
    }
}
