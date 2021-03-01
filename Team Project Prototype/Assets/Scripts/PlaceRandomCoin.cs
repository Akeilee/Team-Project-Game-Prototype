using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceRandomCoin : MonoBehaviour {
    float radius = 30;
    public GameObject coin;
    Vector3 finalPosition;
    bool once;
    bool collided;

    // Start is called before the first frame update
    void Start() {
        once = false;
        collided = false;

        this.GetComponent<SphereCollider>().enabled = true;
        this.GetComponent<MeshCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update() {

        if (GeneratePos() && once == false) {

            if (finalPosition.x <= -13 || finalPosition.x >= 13) {
                GeneratePos();
            }
            else if (finalPosition.z >= 185 || finalPosition.z <= 7) {
                GeneratePos();
            }
            else if (collided == true) {
                GeneratePos();
            }
            else {
                coin.transform.position = new Vector3(finalPosition.x, finalPosition.y + 0.5f, finalPosition.z);
                once = true;
                this.GetComponent<SphereCollider>().enabled = false;
                this.GetComponent<MeshCollider>().enabled = true;
            }

        }


    }

    bool GeneratePos() {

        Vector3 randomDirection = (Random.insideUnitSphere * radius);

        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas)) {
            finalPosition = hit.position;
            return true;
        }
        else {
            finalPosition = Vector3.zero;
            return false;
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Coin") {
            collided = true;
            GeneratePos();
        }
    }
}








/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceRandomCoin : MonoBehaviour {
    float radius = 30;
    public GameObject coin;
    Vector3 finalPosition;
    bool once;
    bool collided;
    bool stophere;
    bool stopCopy;

    public List<Vector3> coinPos = new List<Vector3>();

    public Transform copyCoin;
    GameObject clone;

    // Start is called before the first frame update
    void Start() {
        once = false;
        collided = false;
        stophere = false;
        stopCopy = false;

    }

    // Update is called once per frame
    void Update() {

        SavePos();

        while (stopCopy == false) {
            for (int i = 0; i < coinPos.Count; i++) {
                Debug.Log(i + " " + coinPos[i]);

                if (i == coinPos.Count - 1) {
                    stopCopy = true;
                }

                if (clone != null) {
                    return;
                }
                else { clone = (GameObject)Instantiate(coin, coinPos[i], Quaternion.identity);
                    
                }




            }
        }


    }

    void SavePos() {

        while (stophere == false) {

            for (int i = 0; i < 40; i++) {
                once = false;
                while (GeneratePos() && once == false) {

                    if (finalPosition.x <= -13 || finalPosition.x >= 13) {
                        GeneratePos();
                    }
                    else if (finalPosition.z >= 185 || finalPosition.z <= 7) {
                        GeneratePos();
                    }
                    else if (collided == true) {
                        GeneratePos();
                    }
                    else {
                        //coin.transform.position = new Vector3(finalPosition.x, finalPosition.y + 0.5f, finalPosition.z);
                        coinPos.Add(finalPosition);
                        // Debug.Log(finalPosition);
                        once = true;
                    }

                }

                if (i == 39) {
                    stophere = true;
                }
            }
        }







    }


    bool GeneratePos() {

        Vector3 randomDirection = (Random.insideUnitSphere * radius);

        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas)) {
            finalPosition = hit.position;
            return true;
        }
        else {
            finalPosition = Vector3.zero;
            return false;
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "coin") {
            collided = true;
        }
    }
}
*/