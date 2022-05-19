using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Gun : MonoBehaviour {
    public GameObject bullet;
    public GameObject player;
    

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // public static Object Instantiate(Object original, Vector3 position, Quaternion rotation);
            Instantiate(bullet, player.transform.position + new Vector3(0, .55f, 1f), player.transform.rotation * new Quaternion(0, 90, 90, 0));
        }
    }
}
