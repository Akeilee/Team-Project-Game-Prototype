using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour{

    public float counter;

    void Start(){
        counter = 0;
    }

 
    void Update(){
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Coin") {
            counter++;
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Chest") {
            counter += 2.5f;
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
