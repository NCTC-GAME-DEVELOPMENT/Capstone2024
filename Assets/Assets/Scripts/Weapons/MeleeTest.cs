using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MeleeTest : WeaponBase
{
    // Start is called before the first frame update
    //protected override void Start()
    //{
    //    base.Start();
    //}

    public override void Attack()
    {
        //base.Attack();
        Collider[] colliders = Physics.OverlapSphere(transform.position, weaponData.stats.coneAngle);

        foreach (Collider collider in colliders)
        {
            Vector3 direction = collider.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);
            if(angle <= weaponData.stats.coneAngle / 2)
            {
                if(collider.tag == "enemy")
                    Debug.Log("Hit " + collider.name);
            }
        }    
    }
}
