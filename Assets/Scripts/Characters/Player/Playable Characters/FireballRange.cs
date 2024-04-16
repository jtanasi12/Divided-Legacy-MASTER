using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballRange : MonoBehaviour
{
    [SerializeField]
    AudioSource fireballSoundFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If there is a fireball in range, we will cue the sound effect
        // Should only play once because we are triggering the soundFX on entering the collision only
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fireball"))
        {
            fireballSoundFX.Play(); 
        }

    }
}
