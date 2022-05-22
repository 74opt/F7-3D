using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Gun : MonoBehaviour {
    public GameObject bullet;
    public GameObject player;
    public GameObject camera;
    public GameObject spawner;
    public static Vector3 finalPos;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            RaycastHit hit;
            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            finalPos = hit.point;
            print("final pos:" + finalPos);
            //Vector3 finalPos = Physics.RaycastHit(camera.transform.position, camera.transform.forward, 100f).point;
            // public static Object Instantiate(Object original, Vector3 position, Quaternion rotation);
            Instantiate(bullet, spawner.transform.position, camera.transform.rotation * new Quaternion(0, 90, 90, 0));
        }

        transform.localScale = new Vector3(.18f / player.transform.localScale.x, .45f / player.transform.localScale.y, .12f / player.transform.localScale.z);
    }
}
