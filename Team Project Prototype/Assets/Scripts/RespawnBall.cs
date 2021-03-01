using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBall : MonoBehaviour {
    // Start is called before the first frame update

    private Vector3 initalPos;
    private int counter;
    private float ballTimer;



    void Start() {
        counter = 0;
        initalPos = transform.position;
        ballTimer = 0;
    }

    // Update is called once per frame
    void Update() {
        ballTimer += Time.deltaTime;

        if (ballTimer >= 10 && counter <= 4) {
            this.transform.position = initalPos;
            ballTimer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "StartFloor" && counter <= 4) {

            ballTimer = 0;
            counter++;
            this.transform.position = initalPos;
        
        }
    }
}
