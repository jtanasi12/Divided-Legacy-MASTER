using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line to import the SceneManager namespace


public class MainMenuPlayButton : MonoBehaviour {    
    // Public method to be called when the button is clicked
    public void LoadScene(){
        // Load the scene named "SeansScene"
        SceneManager.LoadScene("Seans Scene"); //TODO swap this with whichever scene we want as our level 1
    }
}
