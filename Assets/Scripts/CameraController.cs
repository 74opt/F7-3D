using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private static Transform transform;
    private static float sensitivity = 5f;
    private static float xRotation;

    public static float yRotation; /*{
        get {return yRotation;}
    }*/

    // Start is called before the first frame update
    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform = GetComponent<Transform>();
        sensitivity = 5f;
    }

    // Update is called once per frame
    private void Update() {
        transform.transform.position = GameObject.Find("Player").transform.position + new Vector3(0, .55f, 0);

        yRotation += Input.GetAxis("Mouse X") * sensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    private void FixedUpdate() {
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
    }
}
