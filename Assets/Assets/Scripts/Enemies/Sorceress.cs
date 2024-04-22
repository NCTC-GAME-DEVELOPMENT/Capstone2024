using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorceress : EnemyBase
{
    [SerializeField] private Transform[] summonPoints;
    [SerializeField] private GameObject enemySummon;
    [SerializeField] private GameObject warning;

    // Start is called before the first frame update
    protected override void InitializeObject()
    {
        base.InitializeObject();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();

    }
}
