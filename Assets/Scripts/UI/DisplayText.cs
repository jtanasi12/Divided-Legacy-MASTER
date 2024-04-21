using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    [SerializeField]
    private Characters player;

    [SerializeField]
    private TextMeshPro textDisplay;

  

    private bool playerHasEntered = false;

    private void Start()
    {
        textDisplay.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !playerHasEntered)
        {
            textDisplay.gameObject.SetActive(true);

            playerHasEntered = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            textDisplay.gameObject.SetActive(false);
        }
    }
}
