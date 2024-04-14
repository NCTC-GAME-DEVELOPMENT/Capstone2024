using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crawler : EnemyBase
{
    private float animTime = 0.771f;
    private float restart = 0.771f;
    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();
        animTime -= Time.deltaTime;
        if (animTime <= 0)
        {
            navMeshAgent.speed = 0f;
            restart -= Time.deltaTime;
            if(restart <= 0)
            {
                animTime = 0.771f;
                restart = 0.771f;
            }
        }
        if (animTime > 0)
        {
            navMeshAgent.speed = 3f;
        }
    }
}
