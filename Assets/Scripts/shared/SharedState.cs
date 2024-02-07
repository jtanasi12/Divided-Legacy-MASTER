using System;
using UnityEngine;

[CreateAssetMenu]
public class SharedState : ScriptableObject
{
    public Boolean isPaused = false;

    public void togglePause() {isPaused = !isPaused;}
}
