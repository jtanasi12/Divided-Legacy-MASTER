using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    // Public method to be called when the button is clicked
    public void LoadScene()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
