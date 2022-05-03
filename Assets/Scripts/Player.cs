using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private static Rigidbody rigidbody;
    private static Transform transform;
    private static Collider collider;
    private static float velocity;
    private static float xVelocity;
    private static float zVelocity;
    private static bool isGrounded;
    private static PlayerState playerState;
    //private static GameObject camera;

    private enum PlayerState {
        Normal, Sprint, Crouch
    }

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        collider = GetComponent<Collider>();
    }

    private void Awake() {
        // if (camera == null) {
        //     camera = GameObject.FindGameObjectWithTag("Player Camera");
        // }
    }

    private void Update() {
        if (Input.GetKey(KeyCode.W)) {
            zVelocity = velocity;
        } else if (Input.GetKey(KeyCode.S)) {
            zVelocity = -velocity;
        } else {
            zVelocity = 0;
        }

        if (Input.GetKey(KeyCode.A)) {
            xVelocity = -velocity;
        } else if (Input.GetKey(KeyCode.D)) {
            xVelocity = velocity;
        } else {
            xVelocity = 0;
        }

        switch (playerState) {
            case PlayerState.Normal:
                velocity = .2f;
                break;
            case PlayerState.Sprint:
                velocity = .3f;
                break;
            case PlayerState.Crouch:
                velocity = .05f;
                break;
        }
    }

    private void FixedUpdate() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y/* + 0.1f*/);

        transform.eulerAngles = new Vector3(0, CameraController.yRotation, 0);

        transform.Translate(new Vector3(xVelocity, 0, zVelocity), Space.Self);
    }
}
