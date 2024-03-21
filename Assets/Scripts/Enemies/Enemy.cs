using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private EnemyDamage enemyDamage;

    public GameObject attackObjectPrefab;
    public float coolDown = 2.5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = coolDown;
    }

    // Start is called before the first frame update
    void Awake()
    {
        SetCharacterName("Enemy");

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ShootProjectile();
            timer = coolDown;
        }

        enemyController.InputMechanics();
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        enemyDamage.DealDamage(collision);
    }

    //virtual so that it can be realized with a different prefab
    public virtual void ShootProjectile()
    {
        Instantiate(attackObjectPrefab, transform.position, Quaternion.identity);
    }

}
