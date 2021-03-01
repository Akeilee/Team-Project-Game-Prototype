using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreenAI : MonoBehaviour {

    public GameObject player;
    public GameObject AI;

    public GameObject resultUI;
    public Text coinText;
    public Text coinTextAI;
    public Text timer;
    public Text winLose;
    private CoinCounter coinScript;
    private CoinCounter coinScriptAI;
    private FinishGame finishGameScript;

    public GameObject pauseUI;

    // Start is called before the first frame update
    void Start() {
        coinScript = player.GetComponent<CoinCounter>();
        coinScriptAI = AI.GetComponent<CoinCounter>();

        finishGameScript = player.GetComponent<FinishGame>();

        resultUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update() {

        DisplayStats();

    }

    private void DisplayStats() {

        if (finishGameScript.finished == true || finishGameScript.timeRemaining<=0) {

            pauseUI.SetActive(false);

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            resultUI.SetActive(true);

            if (finishGameScript.timeRemaining <= 0 || (coinScript.counter < coinScriptAI.counter)) {
                winLose.text = "You Lose!";
            }
            else if (coinScript.counter > coinScriptAI.counter) {
                winLose.text = "You Win!";
            }
            else {
                winLose.text = "Draw!";
            }



            coinText.text = ("Score : " + coinScript.counter.ToString());
            coinTextAI.text = ("AI Score : " + coinScriptAI.counter.ToString());

            int minutes = Mathf.FloorToInt(finishGameScript.timeRemaining / 60F);
            int seconds = Mathf.FloorToInt(finishGameScript.timeRemaining - minutes * 60);
            timer.text = ("Time Left : " + string.Format("{0:0}:{1:00}", minutes, seconds));
        }

    }

    public void Replay() {
        finishGameScript.finished = false;
        resultUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        finishGameScript.finished = false;
        resultUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }
}

