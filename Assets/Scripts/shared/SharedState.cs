using System;
using UnityEngine;

[CreateAssetMenu(menuName ="State Objects/Shared Game State")]
public class SharedState : ScriptableObject
{
    public Boolean isPaused = false;
    public Boolean isCloudBoyActivePlayer = true;

    public void toggleControl() {isCloudBoyActivePlayer = !isCloudBoyActivePlayer;}


    public void togglePause() {isPaused = !isPaused;}

    private void Awake()
    {
        isPaused = false;
    }
}


