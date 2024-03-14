using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : WeaponController
{
    public float coneAngle = 30f;
    public float coneRange = 15f;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        Collider[] colliders = Physics.OverlapSphere(transform.position, coneAngle);

        foreach (Collider collider in colliders)
        {
            Vector3 direction = collider.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);

            if(angle <= coneAngle / 2)
            {
                if(collider.tag == "enemy")
                    Debug.Log("Hit " + collider.name);
            }
        }    
    }
}
