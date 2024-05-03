using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ArrowWeapon : WeaponBase
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform wpn_Arrow;
    //GameObject arrowOrigin;
    string ARROW_SPAWN = "arrow_Origin";
    WeaponPosition weaponPosition;


    public override void Update()
    {
        base.Update();
        //weaponPosition = GetComponentInParent<WeaponPosition>();
        //transform.rotation = weaponPosition.playerRotation;
    }

    public override void Attack()
    {
        //float arcStart = weaponStats.coneAngle;
        //float increment = weaponStats.coneAngle / weaponStats.amount;
        weaponPosition = GetComponentInParent<WeaponPosition>();
        //transform.rotation = weaponPosition.playerRotation;
        GameObject arrowOrigin = Instantiate(new GameObject(ARROW_SPAWN), transform.position, weaponPosition.playerRotation);
        Transform originTransform = arrowOrigin.transform;

        for (int i = 0; i < weaponStats.amount; i++)
        {
            
            //float angle = i * ((Mathf.PI*2)/4) / weaponStats.amount;
            float angle = ((weaponStats.coneAngle * Mathf.Deg2Rad) / weaponStats.amount) * i;
            float x = Mathf.Cos(angle) * weaponStats.coneRange;
            float z = Mathf.Sin(angle) * weaponStats.coneRange;
            float angleDegrees = 90/ (i+1);
            Vector3 pos = transform.position + new Vector3(x, 1, z);
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject arrowObject = Instantiate(arrowPrefab, pos, rot);

            ArrowCollider arrowCollider = arrowObject.GetComponent<ArrowCollider>();
            arrowCollider.damage = weaponStats.damage;
            arrowCollider.pierce = weaponStats.pierce;

            //arcStart -= increment;
        }
    }
}
