using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour{

    public bool canPickup;
    public bool pickup;
    public bool hasObject;
    private GameObject collectableObj;
    public GameObject playerHand;

    void Start(){
        canPickup = false;
        pickup = false;
        hasObject = false;
    }


    void Update(){

        if (Input.GetKeyDown(KeyCode.E)) {
            pickup = !pickup;
        }

        if (canPickup == true && hasObject == false) {
            if (pickup == true) {
                collectableObj.GetComponent<Rigidbody>().isKinematic = true; //not affected by forces

                collectableObj.transform.position = playerHand.transform.position;
                collectableObj.transform.rotation = playerHand.transform.rotation;
       
                collectableObj.transform.parent = playerHand.transform; //parent is now player

                collectableObj.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0) ;

                hasObject = true;
            }
 
        }
        if (hasObject == true && pickup == false) {
            collectableObj.GetComponent<Rigidbody>().isKinematic = false; //not affected by forces
            collectableObj.transform.parent = null; //parent is now player
            hasObject = false;
        }




    }


    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Collectable") {
            canPickup = true;
            collectableObj = collision.gameObject;
        }

    }

    private void OnCollisionExit(Collision collision) {
        canPickup = false;
    }



}
