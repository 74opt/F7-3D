using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: jump make constant pls
// jump: https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/#floaty_jump and https://www.youtube.com/watch?v=7KiK0Aqtmzc
// TODO: make more vars for different values
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
    private const float jumpCooldown = .5f;
    private static float jumpTimer = 0;
    private const float standardGravity = 12.753f;
    private const float fallingGravity = 38.259f;
    //private static GameObject camera;

    private enum PlayerState {
        Normal, Sprint, Crouch, Slide
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y + 0.1f);

        //* WASD
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

        //* Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jumpTimer <= 0 && isGrounded) {
            rigidbody.AddForce(Vector3.up * rigidbody.mass * 10, ForceMode.Impulse);
            jumpTimer = jumpCooldown;
        }

        if (jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
        }

        //* States
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

        //* Controls states
        if (Input.GetKey(KeyCode.LeftShift)) {
            playerState = PlayerState.Sprint;
        } else if (Input.GetKey(KeyCode.LeftControl)) {
            if (rigidbody.velocity.x >= .2f) {
                playerState = PlayerState.Slide;
            } else {
                playerState = PlayerState.Crouch;
            }
        } else {
            playerState = PlayerState.Normal;
        }

        // if (rigidbody.velocity.y < 0) {
        //     rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        // } else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
        //     rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        // }
    }

    private void FixedUpdate() {
        transform.eulerAngles = new Vector3(0, CameraController.yRotation, 0);

        transform.Translate(new Vector3(xVelocity, yVelocity, zVelocity), Space.Self);

        if (rigidbody.velocity.y >= 0) {
            rigidbody.AddForce(Vector3.down * rigidbody.mass * standardGravity);
        } else {
            rigidbody.AddForce(Vector3.down * rigidbody.mass * fallingGravity);
        }

        //* Sliding
        if (playerState == PlayerState.Slide) {
            
        }
        //rigidbody.AddForce(Physics.gravity * 1.3f * rigidbody.mass);

        // if (yVelocity < 0 && isGrounded) {
        //     yVelocity = -2;
        // }
    }
}
