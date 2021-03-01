using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinishGame : MonoBehaviour{

    public Rigidbody playerRB;
    private CoinCounter coinScript;

    private PlayersInAltar noOfPlayerScript;
    public GameObject altar;

    public Camera mainCam;
    int minCoins = 0;
    public Text coinText;
    public Text timer;
    public float timeRemaining;
    public bool finished;

    public bool hitAltar;
    bool once;


    // Start is called before the first frame update
    void Start() {
        timeRemaining = 181;
        coinScript = GetComponent<CoinCounter>();

        noOfPlayerScript = altar.GetComponent<PlayersInAltar>();

        finished = false;

        hitAltar = false;
        once = false;
    }

    // Update is called once per frame
    void Update() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining <= 0 && hitAltar == false) {
            if (once == false) {
                coinScript.counter -= 10;
                once = true;
            }
            timeRemaining = 0;
            finished = true;
        }
        else if (timeRemaining <= 0 && hitAltar == true) {
            timeRemaining = 0;
            finished = true;
        }

        coinText.text = ("Score: " + coinScript.counter.ToString());

        MinuteSeconds();


        if (noOfPlayerScript.players < 2) {
            finished = false;
        }

        else if (noOfPlayerScript.players == 2) {
            finished = true;           
        }

    }

    private void MinuteSeconds() {

        int minutes = Mathf.FloorToInt(timeRemaining / 60F);
        int seconds = Mathf.FloorToInt(timeRemaining - minutes * 60);

        if (timeRemaining >= 180) {
            timer.text = ("Time: " + string.Format("{0:0}:{1:00}", 3, 0));
        }
        else {
            timer.text = ("Time: " + string.Format("{0:0}:{1:00}", minutes, seconds));
        }


    }


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "FinishLine" && timeRemaining > 0 && coinScript.counter >= minCoins) {
            Debug.Log(this.name + " Reached Finish Line " + coinScript.counter);


            playerRB.transform.position = new Vector3(-1.62f, 0.5f, 198.86f);

            Vector3 rot = playerRB.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            playerRB.transform.rotation = Quaternion.Euler(rot);

            playerRB.constraints = RigidbodyConstraints.FreezeAll;

            if (noOfPlayerScript.players == 1) {
                coinScript.counter += 2;
            }

            if (noOfPlayerScript.players == 2) {
                playerRB.transform.position = new Vector3(1.62f, 0.5f, 198.86f);

            }

            hitAltar = true;
        }
        else if (other.gameObject.tag == "FinishLine" && coinScript.counter < minCoins) {
            Debug.Log(this.name + " Not enough coins " + coinScript.counter);
        }
    }

    private void OnTriggerExit(Collider other) {
        finished = false;
    }
}
