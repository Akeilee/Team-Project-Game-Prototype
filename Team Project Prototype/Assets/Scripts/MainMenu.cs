using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    
    void Start(){
        
    }

   
    void Update(){
        
    }

    public void SinglePlayer() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AIGame() {
        Debug.Log("Vs AI Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void MultiPlayer() {
        Debug.Log("Multi Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
