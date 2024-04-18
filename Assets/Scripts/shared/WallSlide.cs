using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    [SerializeField]
    AudioSource wallSlideFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                wallSlideFX.Play();
            }

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
