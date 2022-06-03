using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    private static Collider[] colliders;
    private static Transform transform;
    private static Collider collider;

    // Start is called before the first frame update
    void Start() {
        transform = GetComponent<Transform>();
        collider = GetComponent<Collider>();
    }

    void Awake() {
        StartCoroutine(Delete());
        //colliders = Physics.OverlapSphere(transform.position, collider.bounds.extents.magnitude);
    }

    // Update is called once per frame
    void Update() {
        
    }

    IEnumerator Delete() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
