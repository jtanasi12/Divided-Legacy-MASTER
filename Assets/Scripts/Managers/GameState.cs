using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
  public SharedState sharedState;
  UnityEvent TogglePause;
  public GameObject pauseMenuUI;

  [SerializeField]
  private GameObject loseMenuUI;

  [SerializeField]
  private GameObject winMenuuUI;

  [SerializeField]
  private PlayerHealth cloudBoy;

  [SerializeField]
  private PlayerHealth split;

  [SerializeField]
  private PlayableCharacters cloudBoyPlayer;

  [SerializeField]
  private PlayableCharacters splitPlayer;



    [SerializeField]
    private PlayerController cloudBoyController;

    [SerializeField]
    private PlayerController splitController;

    private bool nextLevel = false;

    // TESTING 3.0
    private void Awake()
    {
        pauseMenuUI.SetActive(false);
        loseMenuUI.SetActive(false);
        winMenuuUI.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 1f;

    }


    // Update is called once per frame
    void Update(){
    if (Input.GetKeyDown(KeyCode.Escape)){
      if (sharedState.isPaused) Resume();
      else PauseGame();
    }

     
  }

    private void FixedUpdate()
    {

        if(cloudBoy.GetIsPlayerDead() || split.GetIsPlayerDead())
        {
            // ALlow the death animation to play before pausing
            StartCoroutine(ActivateLoseMenuAfterDelay(1.5f));
        }

        // If both cloud boy and split capture the flags the game is won!
        if (cloudBoyPlayer.GetFlag() && splitPlayer.GetFlag() && !nextLevel)
        {
            winMenuuUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            sharedState.togglePause();
            nextLevel = true;


        }
      


    }

    public void NextLevel()
    {
        Debug.Log("Next level loaded..");
    }

    IEnumerator ActivateLoseMenuAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Activate the lose menu
        loseMenuUI.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;

        Debug.Log("The player has died");
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
