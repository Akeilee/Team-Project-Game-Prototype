using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInAltar : MonoBehaviour{

    public int players;

    // Start is called before the first frame update
    void Start(){
        players = 0;
    }

    // Update is called once per frame
    void Update(){
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2" || other.gameObject.tag == "AI") {        
            players++;    
        }
    }


}
