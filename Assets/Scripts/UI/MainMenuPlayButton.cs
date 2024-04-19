using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line to import the SceneManager namespace


public class MainMenuPlayButton : MonoBehaviour {

    [SerializeField]
    private AudioSource backGroundMusic;

    private void Awake()
    {
        if(backGroundMusic != null)
        {
            backGroundMusic.Play();
        }
    }

    // Public method to be called when the button is clicked
    public void LoadScene(){
        // Load the scene named "Level 1"
        SceneManager.LoadScene("Level 1"); 
    }

}
