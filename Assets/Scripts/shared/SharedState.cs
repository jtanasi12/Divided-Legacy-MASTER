using System;
using UnityEngine;

[CreateAssetMenu(menuName ="State Objects/Shared Game State")]
public class SharedState : ScriptableObject
{
    public Boolean isPaused = false;

    public void togglePause() {isPaused = !isPaused;}
}
