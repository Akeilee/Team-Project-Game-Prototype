using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public float mouseSensitivity = 200f; //speed of mouse
    public Transform player;
    float rotateX = 0.0f;

    float timer;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        timer = 0;
    }

    void Update() {

        timer += Time.deltaTime;

        if (timer >= 1) {
            float getMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float getMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


            //rotateX -= getMouseY;
            //rotateX = Mathf.Clamp(rotateX, -45, 45);
            //transform.localRotation = Quaternion.Euler(rotateX, 0, 0); //rotate on x axis

            player.Rotate(Vector3.up * getMouseX); //rotate on y axis



            if (Input.GetKeyDown(KeyCode.Q)) {
                if (Cursor.lockState == CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.None;

                else if (Cursor.lockState == CursorLockMode.None)
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }


    }

}
