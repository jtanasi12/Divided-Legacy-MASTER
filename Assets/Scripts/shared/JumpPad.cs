using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private float bounce = 20f;

    [SerializeField]
    private AudioSource boingFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            boingFX.Play();
        }
    }
}
