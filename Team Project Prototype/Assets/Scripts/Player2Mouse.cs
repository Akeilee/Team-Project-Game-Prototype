using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Mouse : MonoBehaviour {

    public float mouseSensitivity = 200f; //speed of mouse
    public Transform player;
    float rotateX = 0.0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float getMouseX = Input.GetAxis("MouseXNums") * mouseSensitivity * Time.deltaTime;
        //float getMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        //rotateX -= getMouseY;
        //rotateX = Mathf.Clamp(rotateX, -45, 45);
        //transform.localRotation = Quaternion.Euler(rotateX, 0, 0); //rotate on x axis

        player.Rotate(Vector3.up * getMouseX); //rotate on y axis



        if (Input.GetKeyDown(KeyCode.E)) {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;

            else if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
        }

    }

}