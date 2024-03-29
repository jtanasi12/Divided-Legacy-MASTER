using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public SharedState sharedState;


    public void LoadNextLevel()
    {
        Debug.Log("Next level loading....");

        Time.timeScale = 1f;
        sharedState.togglePause();

       SceneController.instance.NextLevel();

    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Unpause the game before loading the next scene
        Time.timeScale = 1f;

        // Use the SceneController to load the next level
        SceneController.instance.NextLevel();
    }
}
