using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [SerializeField]
    private AudioSource jumpLandFX;

    [SerializeField]
    private AudioSource wallSlideFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with the wall, on entry cue a landing sound
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            jumpLandFX.Play();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            wallSlideFX.Play();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                wallSlideFX.Stop();
            }

        }
    }


}
