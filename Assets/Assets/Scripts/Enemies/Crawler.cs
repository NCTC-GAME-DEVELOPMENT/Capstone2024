using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crawler : EnemyBase
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MoveToPlayer();
    }
}
