using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
  public SharedState sharedState;
  UnityEvent TogglePause;
  public GameObject pauseMenuUI;


  // Update is called once per frame
  void Update(){
    if (Input.GetKeyDown(KeyCode.Escape)){
      if (sharedState.isPaused) Resume();
      else PauseGame();
    }
  }

  public void PauseGame(){
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    sharedState.togglePause();
  }

  public void Resume(){
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1;
    sharedState.togglePause();
  }
  public void GoToMainMenu(){
    //This is where we should route to the main menu when we make one
  }
  public void QuitGame(){
    //This is where we need to make a method to close the application
  }
}
