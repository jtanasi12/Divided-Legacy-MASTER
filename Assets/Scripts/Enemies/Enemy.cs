using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private EnemyDamage enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyController.InputMechanics();
    }

  void OnCollisionStay2D(Collision2D collision)
    {
        enemyDamage.DealDamage(collision);
    }

}
