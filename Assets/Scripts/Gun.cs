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
    private static float fireRateTimer;
    private const float fireRate = .17f;

    // Start is called before the first frame update
    void Start() {
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Mouse0) && fireRateTimer <= 0 && Player.playerState != Player.PlayerState.Sprint) {
            RaycastHit hit;
            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            finalPos = hit.point;
            // if (finalPos == new Vector3(0, 0, 0)) {
            //     finalPos = player.transform.position;
            // }
            //Vector3 finalPos = Physics.RaycastHit(camera.transform.position, camera.transform.forward, 100f).point;
            // public static Object Instantiate(Object original, Vector3 position, Quaternion rotation);
            Instantiate(bullet, spawner.transform.position, camera.transform.rotation * new Quaternion(0, 90, 90, 0));
            fireRateTimer = fireRate;
        } else {
            fireRateTimer -= Time.deltaTime;
        }

        if (Player.playerState == Player.PlayerState.Crouch || Player.playerState == Player.PlayerState.Slide) {
            transform.localScale = new Vector3(.15f, .45f, .18f);
        } else {
            transform.localScale = new Vector3(.15f, .45f, .1f);
        }
        //transform.localScale = new Vector3(.18f / player.transform.localScale.x, .45f / player.transform.localScale.y, .12f / player.transform.localScale.z);

        if (Player.playerState == Player.PlayerState.Sprint) { //TODO this stuff pls
            //transform.rotation = Quaternion.FromToRotation();
            //transform.rotation = new Quaternion(15, player.transform.rotation.y, 90, 90);
            //transform.position 
        } else {
            //transform.rotation = new Quaternion(90, player.transform.rotation.y, 90, 0);
            //transform.position
        }
    }
    
}
