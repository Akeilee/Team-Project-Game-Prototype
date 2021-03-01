using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{

    public static bool paused;
    public GameObject pauseUI;


    void Start(){
        pauseUI.SetActive(false);
        paused = false;
    }


    void Update(){

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (paused == true) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    private void ResumeGame() {
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    private void PauseGame() {
        Cursor.lockState = CursorLockMode.None;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }


    public void Restart() {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
