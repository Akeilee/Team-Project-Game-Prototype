using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen2 : MonoBehaviour {

    public GameObject player;
    public GameObject player2;
    public GameObject resultUI;
    public Text coinText;
    public Text coinText2;
    public Text timer;
    public Text winLose;
    private CoinCounter coinScript;
    private CoinCounter coinScript2;
    private FinishGame finishGameScript;

    private PlayersInAltar noOfPlayerScript;
    public GameObject altar;

    public GameObject pauseUI;

    // Start is called before the first frame update
    void Start() {
        coinScript = player.GetComponent<CoinCounter>();
        coinScript2 = player2.GetComponent<CoinCounter>();
        finishGameScript = player.GetComponent<FinishGame>();

        noOfPlayerScript = altar.GetComponent<PlayersInAltar>();

        resultUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update() {

        DisplayStats();

    }

    private void DisplayStats() {

        if (finishGameScript.finished == true || finishGameScript.timeRemaining <= 0) {

            pauseUI.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            resultUI.SetActive(true);

            if (finishGameScript.timeRemaining <= 0) {
                if (coinScript.counter > coinScript2.counter) {
                    winLose.text = "Player 1 Wins!";
                }
                else if (coinScript.counter < coinScript2.counter) {
                    winLose.text = "Player 2 Wins!";
                }
                else {
                    winLose.text = "Draw!";
                }

            }
            else {

                if (coinScript.counter > coinScript2.counter) {
                    winLose.text = "Player 1 Wins!";
                }
                else if (coinScript.counter < coinScript2.counter) {
                    winLose.text = "Player 2 Wins!";
                }
                else {
                    winLose.text = "Draw!";
                }
            }



            coinText.text = ("Player 1   -  " + coinScript.counter.ToString());
            coinText2.text = ("Player 2   -  " + coinScript2.counter.ToString());

            int minutes = Mathf.FloorToInt(finishGameScript.timeRemaining / 60F);
            int seconds = Mathf.FloorToInt(finishGameScript.timeRemaining - minutes * 60);
            timer.text = ("Time Left : " + string.Format("{0:0}:{1:00}", minutes, seconds));
        }

    }

    public void Replay() {
        //noOfPlayerScript.players = 0;
        finishGameScript.finished = false;
        resultUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        //noOfPlayerScript.players = 0;
        finishGameScript.finished = false;
        resultUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }
}
