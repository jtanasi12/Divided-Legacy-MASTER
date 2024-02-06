using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Resume (){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
    public void GoToMainMenu (){
        //This is where we should route to the main menu when we make one
    }
    public void QuitGame (){
        //This is where we need to make a method to close the application
    }
}
