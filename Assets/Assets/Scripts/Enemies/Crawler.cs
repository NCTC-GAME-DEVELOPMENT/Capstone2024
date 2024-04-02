using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crawler : EnemyBase
{
    public float health = 25;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MoveToPlayer();
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Crawler is at " + health + "HP");
        if(health <= 0)
            Death();
    }
}
