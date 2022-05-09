using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: jump make constant pls
// jump: https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/#floaty_jump and https://www.youtube.com/watch?v=7KiK0Aqtmzc
public class Player : MonoBehaviour {
    private static Rigidbody rigidbody;
    private static Transform transform;
    private static Collider collider;
    private static float velocity;
    private static float xVelocity;
    private static float yVelocity;
    private static float zVelocity;
    private static bool isGrounded;
    private static PlayerState playerState;
    private static float fallMultiplier = 4f;
    private static float lowJumpMultiplier = 2.3f;
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

        if (Input.GetKey(KeyCode.Space)) {
            if (isGrounded) {
                rigidbody.AddForce(Vector3.up * rigidbody.mass * .6f, ForceMode.Impulse);
            }
        }

        switch (playerState) {
            case PlayerState.Normal:
                velocity = .2f;
                transform.localScale = new Vector3(1.2f, 1.8f, 1.2f);
                break;
            case PlayerState.Sprint:
                velocity = .4f;
                transform.localScale = new Vector3(1.2f, 1.8f, 1.2f);
                break;
            case PlayerState.Crouch:
                velocity = .05f;
                transform.localScale = new Vector3(1.2f, 1f, 1.2f);
                break;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            playerState = PlayerState.Sprint;
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            playerState = PlayerState.Crouch;
        } else {
            playerState = PlayerState.Normal;
        }

        if (rigidbody.velocity.y < 0) {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y + 0.1f);

        transform.eulerAngles = new Vector3(0, CameraController.yRotation, 0);

        transform.Translate(new Vector3(xVelocity, yVelocity, zVelocity), Space.Self);

        // if (yVelocity < 0 && isGrounded) {
        //     yVelocity = -2;
        // }
    }
}
