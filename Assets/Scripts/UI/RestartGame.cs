using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class RestartGame: MonoBehaviour
{
    public void RetryGame()
    {
        Debug.Log("Restarting the game");
        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1; // Unpause the game 
    }
}
