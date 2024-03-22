using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{

    /*
      public GameObject attackObjectPrefab;
    public float coolDown = 2.5f;
    private float timer; ************** SEAN **************
    */

    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private EnemyDamage enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        // timer = coolDown; ************** SEAN **************

    }

    // Start is called before the first frame update
    void Awake()
    {
        SetCharacterName("Enemy");


    }

    // Update is called once per frame
    void Update()
    {
        /*
         *************** SEAN **************
         * 
        timer -= Time.deltaTime;
        if(timer <= 0){
            shootProjectile();
            timer = coolDown;
        }
        */

        enemyController.InputMechanics();
    }


    /*  

   **** SEAN ******
 //virtual so that it can be realized with a different prefab
     public virtual void shootProjectile(){
         Instantiate(attackObjectPrefab, transform.position, Quaternion.identity);
     }

       void OnCollisionStay2D(Collision2D collision)
     {
         enemyDamage.DealDamage(collision);
     }


     */




}
