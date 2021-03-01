using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCube : MonoBehaviour{

    private Vector3 direction = Vector3.left;
    public float speed = 15;
    float maxLeft = -13.38f;
    float maxRight = 13.38f;
    public int random;

    void Start(){
        random = Random.Range(1, 11);

        if (random % 2 == 0 ) { //if even
            direction = Vector3.right;
        }
        else {
            direction = Vector3.left;
        }
    }

    
    void Update() {

        transform.Translate(direction * speed * Time.deltaTime);


        if (transform.position.x <= maxLeft) {
            direction = Vector3.right;
        }

        else if (transform.position.x >= maxRight) {
            direction = Vector3.left;
        }

    }
}
