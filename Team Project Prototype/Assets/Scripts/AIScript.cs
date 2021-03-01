using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIScript : MonoBehaviour {

    public Camera cam;
    public NavMeshAgent agent;
    public Rigidbody rb;

    public bool slowDown;
    public bool stopHere;

    private PlayersInAltar noOfPlayerScript;
    public GameObject altar;
    private CoinCounter coinScript;

    bool once;

    List<GameObject> coinList = new List<GameObject>();

    bool getCoin;
    Vector3 coinPos;
    bool slope;


    void Start() {
        //agent.updateRotation = false;

        slowDown = false;
        stopHere = false;

        noOfPlayerScript = altar.GetComponent<PlayersInAltar>();
        coinScript = GetComponent<CoinCounter>();
        once = false;

        coinList.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        coinList.AddRange(GameObject.FindGameObjectsWithTag("Chest"));
        getCoin = false;

        slope = false;
    }

    void FixedUpdate() {
        rb.AddForce(Physics.gravity);
    }


    // Update is called once per frame
    void Update() {

        //if (Input.GetMouseButtonDown(0)) {
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit)) {
        //        agent.SetDestination(hit.point);
        //        
        //    }
        //}

        if (rb.transform.position.z >= 53.6f && rb.transform.position.z <= 62.6f) {
            slowDown = true;
        }
        else if (rb.transform.position.z >= 151 && rb.transform.position.z <= 188) {
            slowDown = true;
            slope = true;
        }
        else {
            slowDown = false;
            slope = false;
        }





        if (stopHere == false) { //not reach finish line

            foreach (GameObject go in coinList) {
                if (go == null) {
                    break;
                }
                else if (Vector3.Distance(transform.position, go.transform.position) < 6) {
                    Debug.Log("Go for Coin");

                    coinPos = go.transform.position;
                    coinList.Remove(go);
                    getCoin = true;
                    break;
                }
            }


            if (getCoin == true) {
                agent.SetDestination(coinPos);
            }
            else if (rb.transform.position.z < 48 && getCoin == false) {
                agent.SetDestination(new Vector3(-0.94f, 14.523f, 48.7f));
            }
            else if (rb.transform.position.z >= 48 && rb.transform.position.z < 106 && getCoin == false) {
                agent.SetDestination(new Vector3(0, 28.8f, 106.7f));
            }
            else if (rb.transform.position.z >= 106 && rb.transform.position.z < 119 && getCoin == false) {
                agent.SetDestination(new Vector3(0.29f, 30.08f, 119.74f));
            }
            else if (rb.transform.position.z >= 119 && rb.transform.position.z < 150 && getCoin == false) {
                agent.SetDestination(new Vector3(4.75f, 35.017f, 150.48f));
            }
            else if (rb.transform.position.z >= 150 && rb.transform.position.z < 195 && getCoin == false) {
                agent.SetDestination(new Vector3(0.3f, 0.76f, 195.8f));
            }
            else if (rb.transform.position.z >= 195 && getCoin == false) {
                stopHere = true;
            }



            if (agent.remainingDistance > agent.stoppingDistance) { //not at destination
                if (slowDown == true) { //on water

                    //rb.velocity = agent.desiredVelocity / 2;
                    if (slope == true) {
                        agent.speed = 4;
                    }
                    else {
                        agent.speed = 3;
                    }

                }

                else
                    agent.speed = 6;
                rb.velocity = agent.desiredVelocity;

            }
            else { //at destination
                getCoin = false;
                rb.velocity = Vector3.zero;
            }

        }


        else { //reached finish line

            if (once == false) {
                rb.velocity = Vector3.zero;
                rb.transform.position = new Vector3(-1.62f, 0.5f, 198.86f);
                agent.enabled = false;

                if (noOfPlayerScript.players == 2) {
                    rb.transform.position = new Vector3(1.62f, 0.5f, 198.86f);

                }

                once = true;
            }


        }




    }



    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pond") {
            slowDown = true;
        }
        if (other.gameObject.tag == "FinishLine") {
            stopHere = true;

        }

    }


    private void OnTriggerExit(Collider other) {
        slowDown = false;

    }

}
