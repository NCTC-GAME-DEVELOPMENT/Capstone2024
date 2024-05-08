using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Slime : EnemyBase
{
    [SerializeField] private Transform raycastSpawnPoint;
    public bool hasSplit;
    public GameObject slimePool;
    private float animTime = 0.583f;
    private float idleTime = 0.583f;
    private bool trigger = false;
    void Start()
    {
        base.InitializeObject();

        if(!hasSplit)
        {
            health = StatsChart[StatsChartRow].health;
            contactDamage = StatsChart[StatsChartRow].contactDamage;
        }
        else
        {
            health = StatsChart[StatsChartRow].health / 2;
            contactDamage = StatsChart[StatsChartRow].contactDamage / 2;
        }
    }

    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();
        animTime -= Time.deltaTime;
        if (animTime <= 0)
        {
            navMeshAgent.speed = 0f;
            if (!trigger)
            {
                animator.SetTrigger("isIdle");
                trigger = true;
            }
            idleTime -= Time.deltaTime;
            if (idleTime <= 0)
            {
                trigger = false;
                animator.SetTrigger("isMoving");
                animTime = 0.583f;
                idleTime = 0.583f;
            }
        }
        if (animTime > 0)
        {
            navMeshAgent.speed = 3f;
        }
    }
    protected override void Death()
    {
        base.Death();

        if (!hasSplit)
        {
            for (int i = 0; i < 2; i++)
            {

                GameObject smallSlime = Instantiate(gameObject, transform.position, transform.rotation);
                smallSlime.transform.localScale *= 0.5f;
                smallSlime.GetComponent<ColorChange>().ChangeToOriginal();
                smallSlime.GetComponent<Animator>().enabled = true;
                smallSlime.GetComponent<Slime>().hasSplit = true;
                smallSlime.GetComponent<EnemyBase>().enabled = true;
                smallSlime.GetComponent<NavMeshAgent>().enabled = true;
            }
        }

        RaycastHit hit;
        {
            if (Physics.Raycast(raycastSpawnPoint.position, -Vector3.up, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                GameObject damagingCircle = Instantiate(slimePool, hit.point, Quaternion.identity);
                Destroy(damagingCircle, 5f);
            }
        }
    }
}
