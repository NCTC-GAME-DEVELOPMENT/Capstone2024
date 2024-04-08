using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : EnemyBase
{
    public bool hasSplit;
    public GameObject slimePool;
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

    protected override void Death()
    {
        base.Death();

        if (!hasSplit)
        {
            for (int i = 0; i < 2; i++)
            {

                GameObject smallSlime = Instantiate(gameObject, transform.position, transform.rotation);
                smallSlime.transform.localScale *= 0.5f;

                smallSlime.GetComponent<Slime>().hasSplit = true;
                smallSlime.GetComponent<EnemyBase>().enabled = true;
                smallSlime.GetComponent<NavMeshAgent>().enabled = true;
            }
        }

        Vector3 spawnPosition = new Vector3(transform.position.x, -.25f, transform.position.z);

        GameObject damagingCircle = Instantiate(slimePool, spawnPosition, Quaternion.identity);
        Destroy(damagingCircle, 5f);
    }

}
