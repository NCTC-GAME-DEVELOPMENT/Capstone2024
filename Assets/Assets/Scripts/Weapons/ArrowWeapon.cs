using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWeapon : WeaponBase
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform wpn_Arrow;

    WeaponPosition weaponPosition;


    public override void Update()
    {
        base.Update();
        weaponPosition = GetComponentInParent<WeaponPosition>();
        transform.rotation = weaponPosition.playerRotation;
    }

    public override void Attack()
    {
        //float arcStart = weaponStats.coneAngle;
        //float increment = weaponStats.coneAngle / weaponStats.amount;

        for (int i = 0; i < weaponStats.amount; i++)
        {
            float angle = i * ((Mathf.PI*2)/12) / weaponStats.amount;
            float x = Mathf.Cos(angle) * weaponStats.coneRange;
            float z = Mathf.Sin(angle) * weaponStats.coneRange;
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Vector3 pos = transform.position + new Vector3(x, 1, z);
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject arrowObject = Instantiate(arrowPrefab, pos, rot * transform.rotation);

            ArrowCollider arrowCollider = arrowObject.GetComponent<ArrowCollider>();
            arrowCollider.damage = weaponStats.damage;
            arrowCollider.pierce = weaponStats.pierce;

            //arcStart -= increment;
        }
    }
}
