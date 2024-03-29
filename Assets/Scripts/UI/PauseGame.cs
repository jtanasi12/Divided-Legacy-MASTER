using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenuUI;

   [SerializeField]
    private SharedState sharedState;

   public void UnPauseGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        sharedState.togglePause();

    }
}
