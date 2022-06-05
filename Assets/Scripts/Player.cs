using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// MAKE A HORDE SHOOTER
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
    public static PlayerState playerState;
    private static float fallMultiplier = 4f;
    private static float lowJumpMultiplier = 2.3f;
    // private const float jumpCooldown = .5f;
    // private static float jumpTimer = 0;
    private const float standardGravity = 12.753f;
    private const float fallingGravity = 38.259f;
    private bool doubleJump = true;
    public static float health = 300;
    
    [Header("Wallrunning variables")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public bool wallLeft;
    public bool wallRight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private float wallrunTimer;
    private const float wallrunTimerAmount = 1;
    //private static GameObject camera;

    //* feel like i need this public for something soon
    public enum PlayerState {
        Normal, Sprint, Crouch, Slide
    }

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        collider = GetComponent<Collider>();
        rigidbody.freezeRotation = true;
        playerState = PlayerState.Normal;
    }

    private void Awake() {
        // if (camera == null) {
        //     camera = GameObject.FindGameObjectWithTag("Player Camera");
        // }
    }

    private void Update() {
        if (health <= 0) {
            // TODO: losing
            Time.timeScale = 0;
        }

        //? should this be here?
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        //health -= .01f;

        //print(velocity);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y + .7f);

        if (!doubleJump && (isGrounded || wallRight || wallLeft)) {
            doubleJump = true;
        }
        //TODO: fix double jumping
        //TODO: sliding
        //isGrounded = Physics.CheckBox(new Vector3(transform.position.x, transform.position.y - collider.bounds.extents.y, transform.position.z), new Vector3(collider.bounds.extents.x, collider.bounds.extents.y, collider.bounds.extents.z));

        //* WASD
        if (Input.GetKey(KeyCode.W)) {
            zVelocity = velocity;
        } else if (Input.GetKey(KeyCode.S)) {
            zVelocity = -velocity;
        } else {
            zVelocity = 0;
        }

        if (Input.GetKey(KeyCode.A) && !wallLeft) {
            xVelocity = -velocity;
        } else if (Input.GetKey(KeyCode.D) && !wallRight) {
            xVelocity = velocity;
        } else {
            xVelocity = 0;
        }

        //* Jumping
        if (Input.GetKeyDown(KeyCode.Space) && /*jumpTimer <= 0 &&*/ (isGrounded || doubleJump || wallLeft || wallRight)) {
            if (playerState == PlayerState.Slide) {
                velocity += .75f;
            }
            if (wallLeft) {
                //print("left");
                transform.position = new Vector3(leftWallhit.point.x + transform.right.x, transform.position.y, transform.position.z);
                velocity += 3;
                doubleJump = true;
            } else if (wallRight) {
                //print("right");
                transform.position = new Vector3(rightWallhit.point.x - transform.right.x, transform.position.y, transform.position.z);
                velocity += 3;
                doubleJump = true;
            }

            if (doubleJump) {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            }
            rigidbody.AddForce(transform.up * rigidbody.mass * 12, ForceMode.Impulse);

            if (!isGrounded) {
                doubleJump = false;
            }
        }

        // if (jumpTimer > 0) {
        //     jumpTimer -= Time.deltaTime;
        // }

        //* States
        //TODO: find some way to slow down player on big turns to nerf the speed gain
        switch (playerState) {
            case PlayerState.Normal:
                if (velocity > 8) {
                    velocity -= .5f;
                } else {
                    velocity = 8f;
                }
                transform.localScale = new Vector3(1.2f, 1.8f, 1.2f);
                break;
            case PlayerState.Sprint:
                if (velocity > 16) {
                    velocity -= .5f;
                } else {
                    velocity = 16f;
                }
                transform.localScale = new Vector3(1.2f, 1.8f, 1.2f);
                break;
            case PlayerState.Crouch:
                velocity = 2f;
                transform.localScale = new Vector3(1.2f, 1f, 1.2f);
                break;
            case PlayerState.Slide:
                if (velocity > 2 && isGrounded) {
                    velocity -= .02f;
                }/* else {
                    playerState = PlayerState.Crouch;
                }*/
                transform.localScale = new Vector3(1.2f, 1f, 1.2f);
                break;
            // case PlayerState.Slide:
            //     transform.localScale = new Vector3(1,2f, 1f, 1.2f);
            //     break;
        }

        //* Controls states
        //playerState = PlayerState.Normal;

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (playerState != PlayerState.Sprint) {
                playerState = PlayerState.Sprint;
            } else {
                playerState = PlayerState.Normal;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            if (velocity > 2) { // FIXME always put on slide, even when not moving
                playerState = PlayerState.Slide;
            } else {
                playerState = PlayerState.Crouch;
            }
        } else if (Input.GetKeyUp(KeyCode.LeftControl)) {
            playerState = PlayerState.Normal;
        }

        // if (rigidbody.velocity.y < 0) {
        //     rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        // } else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
        //     rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        // }
        //print("xVel: " + xVelocity + " yVel:" + yVelocity + " zVel:" + zVelocity);
        //print(isGrounded);

        //transform.eulerAngles = new Vector3(0, CameraController.yRotation, 0);

        //transform.Translate(new Vector3(xVelocity, yVelocity, zVelocity), Space.Self);
        //* This will detect walls
        // TODO: https://www.youtube.com/watch?v=gNt9wBOrQO4
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallhit, 1f, whatIsWall) && !isGrounded;
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallhit, 1f, whatIsWall) && !isGrounded;

        // if (wallRight) {
        //     // rigidbody.AddForce(Vector3.right * rigidbody.mass * 10, ForceMode.Impulse);
        //     transform.rotation = Quaternion.Euler(0, 0, 15);
        //     print("right");
        // }
        
        // if (wallLeft) {
        //     // rigidbody.AddForce(Vector3.left * rigidbody.mass * 10, ForceMode.Impulse);
        //     transform.rotation = Quaternion.Euler(0, 0, -15);
        //     print("left");
        // }

        //! velocity limit
        if (velocity > 150) {
            velocity = 150;
        }
    }

    private void FixedUpdate() {
        if (wallRight) {
            transform.eulerAngles = new Vector3(0, CameraController.yRotation, 15);
            rigidbody.AddForce(transform.right * rigidbody.mass * 10, ForceMode.Impulse);
            //print("right");
            rigidbody.AddForce(Vector3.down * rigidbody.mass, ForceMode.Impulse);
            rigidbody.velocity = transform.TransformDirection(zVelocity * .5f, 0, zVelocity);
        } else if (wallLeft) {
            transform.eulerAngles = new Vector3(0, CameraController.yRotation, -15);
            rigidbody.AddForce(-transform.right * rigidbody.mass * 10, ForceMode.Impulse);
            //rigidbody.AddForce(Vector3.down * rigidbody.mass, ForceMode.Impulse);
            //print("left");
            rigidbody.velocity = transform.TransformDirection(zVelocity * .5f, 0, zVelocity);
        } else {
            transform.eulerAngles = new Vector3(0, CameraController.yRotation, 0);
            rigidbody.velocity = transform.TransformDirection(new Vector3(xVelocity, rigidbody.velocity.y, zVelocity));
        }

        //transform.Translate(new Vector3(xVelocity, yVelocity, zVelocity), Space.Self);
        // rigidbody.velocity = transform.TransformDirection(new Vector3(xVelocity, rigidbody.velocity.y, zVelocity));
        //rigidbody.AddForce(new Vector3(xVelocity, 0, zVelocity), ForceMode.VelocityChange);

        if (rigidbody.velocity.y >= 0) {
            rigidbody.AddForce(Vector3.down * rigidbody.mass * standardGravity);
        } else {
            rigidbody.AddForce(Vector3.down * rigidbody.mass * fallingGravity);
        }

        //rigidbody.AddForce(Physics.gravity * 1.3f * rigidbody.mass);

        // if (yVelocity < 0 && isGrounded) {
        //     yVelocity = -2;
        // }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            health -= 20;
        }
    }
}
