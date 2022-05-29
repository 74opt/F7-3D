using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    private Transform transform;

    // Start is called before the first frame update
    void Start() {
        transform = GetComponent<Transform>();
        InvokeRepeating("Spawn", Random.Range(0f, 8f), Random.Range(5f, 8f));
    }

    private void Spawn() {
        Instantiate(enemy, transform);
    }
}
