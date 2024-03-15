using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int currentHealth; // Keep track of current health

    protected bool playerIsDead = false;

    [SerializeField]
    protected MainAnimationController playerAnimation;

    public int GetHealth()
    {

        return currentHealth;
    }
}
