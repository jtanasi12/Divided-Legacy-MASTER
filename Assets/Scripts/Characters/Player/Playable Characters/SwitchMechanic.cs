using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMechanic : MonoBehaviour
{
    public GameObject cloudBoy;
    public GameObject split;

    [SerializeField]
    private PlayerHealth cloudBoyHealth;

    [SerializeField]
    private Enemy[] heavenEnemies;

    [SerializeField]
    private Enemy[] hellEnemies;

    [SerializeField]
    private ShareHealthCoin[] heavenCoins;

    [SerializeField]
    private ShareHealthCoin[] hellCoins;

    [SerializeField]
    private HeartPickUp[] heavenHearts;

    [SerializeField]
    private HeartPickUp[] hellHearts;


    [SerializeField]
    private PlayerHealth splitHealth;

    public bool isCloudBoyActive = true;

    private bool flagIsCaptured = false;

    private void Awake()
    {
        
        split.GetComponent<PlayableCharacters>().enabled = false;

        split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (cloudBoy.GetComponent<PlayableCharacters>().GetFlag() && !flagIsCaptured || split.GetComponent<PlayableCharacters>().GetFlag() && !flagIsCaptured)
        {
            SwitchPlayer();

            flagIsCaptured = true;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift) && !flagIsCaptured)
            {
                SwitchPlayer();
            }

        }
    }

    private void SwitchPlayer(){

        isCloudBoyActive = !isCloudBoyActive;


        if (isCloudBoyActive)
        {
            // If we switch to Cloudboy set Split inactive
            SwitchSplitInactive();
            SwitchCloudBoyActive();

            cloudBoy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;


            // Loop through heaven enemies to unfreeze their rotation 
            foreach (Enemy heavenEnemy in heavenEnemies)
            {
               if(heavenEnemy != null)
                {
                    Debug.Log("UFreezing heaven enemies");
                    heavenEnemy.GetComponent<EnemyController>().SetSwitchedState(false);
                }

            }

            // Freeze hell hearts 
            foreach (HeartPickUp hearts in hellHearts)
            {
                if(hearts != null)
                {
                    hearts.SetSwitchedState(true);
                }

            }

            // Freeze Hell Share Health Coins
            foreach (ShareHealthCoin coin in hellCoins)
            {
                if (coin != null)
                {
                    coin.SetSwitchedState(true);
                }

            }


            // Unfreeze heaven Share Health Coins
            foreach (ShareHealthCoin coins in heavenCoins)
            {
                if (coins != null)
                {
                   coins.SetSwitchedState(false);
                }
            }

            // Unfreeze heaven hearts 
            foreach (HeartPickUp hearts in heavenHearts)
            {
               if(hearts != null)
                {
                    hearts.SetSwitchedState(false);
                }
            }


            // Loop through hell enemies to freeze their rotation 
            foreach (Enemy hellEnemy in hellEnemies)
            {
                if (hellEnemy != null)
                {
                    Debug.Log("UFreezing hell enemies");
                    hellEnemy.GetComponent<EnemyController>().SetSwitchedState(true);

                    hellEnemy.GetComponent<MainAnimationController>().SetIdleState();
                }
             

            }

            // If the player is dead, we do not want to switch the animataion
            if (!splitHealth.GetIsPlayerDead())
            {
                split.GetComponent<SplitAnimations>().SetIdleState();

            }

        }
        else 
        {
            // If we switch to Split, set CloudBoy inactive
            SwitchCloudBoyInactive();
            SwitchSplitActive();

            split.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            cloudBoy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            // Loop through heaven enemies to freeze their rotation 
            foreach(Enemy enemy in heavenEnemies)
            {
                if (enemy != null)
                {
                    Debug.Log("Freezing heaven enemies");
                    enemy.GetComponent<EnemyController>().SetSwitchedState(true);

                    enemy.GetComponent<MainAnimationController>().SetIdleState();
                }
               

            }

            // Freeze heaven hearts 
            foreach (HeartPickUp hearts in heavenHearts)
            {
                if( hearts != null)
                {
                    hearts.SetSwitchedState(true);
                }

            }


            // Freeze heaven Share Health Hearts 
            foreach (ShareHealthCoin coins in heavenCoins)
            {
                if (coins != null)
                {
                    coins.SetSwitchedState(true);
                }

            }

            // Unfreeze hell hearts 
            foreach (HeartPickUp hearts in hellHearts)
            {
                if(hearts != null)
                {
                    hearts.SetSwitchedState(false);
                }
            }

            // Unfreeze hell Share Health Hearts 
            foreach (ShareHealthCoin coins in hellCoins)
            {
                if (coins != null)
                {
                    coins.SetSwitchedState(false);
                }
            }



            // Loop through hell enemies to ufreeze their rotation 
            foreach (Enemy hellEnemy in hellEnemies)
            {
              if(hellEnemy != null) {

                  Debug.Log("Freezing hell enemies");
                  hellEnemy.GetComponent<EnemyController>().SetSwitchedState(false);

                }

            }

            // If the player is dead, we do not want to switch the animataion
            if (!cloudBoyHealth.GetIsPlayerDead())
            {
             
                cloudBoy.GetComponent<CloudBoyAnimations>().SetIdleState();

            }


        }
        cloudBoy.GetComponent<PlayableCharacters>().enabled = isCloudBoyActive;

        split.GetComponent<PlayableCharacters>().enabled = !isCloudBoyActive;



    }

    private void SwitchCloudBoyInactive()
    {
        cloudBoy.layer = LayerMask.NameToLayer("SwitchedState");

        cloudBoy.GetComponent<PlayableCharacters>().SetCharacterState(true);

        Debug.Log("ClOUDBOY INACTIVE");
    }

    private void SwitchSplitInactive()
    {
        split.layer = LayerMask.NameToLayer("SwitchedState");

        Debug.Log("SPLIT INACTIVE");

        split.GetComponent<PlayableCharacters>().SetCharacterState(true);
    }

    private void SwitchCloudBoyActive()
    {
        cloudBoy.layer = LayerMask.NameToLayer("Player");

        cloudBoy.GetComponent<PlayableCharacters>().SetCharacterState(false);

        Debug.Log("CLOUDBOY ACTIVE");

    }

    private void SwitchSplitActive()
    {
        split.layer = LayerMask.NameToLayer("Player");

        split.GetComponent<PlayableCharacters>().SetCharacterState(false);

        Debug.Log("SPLIT ACTIVE");


    }

  

}
