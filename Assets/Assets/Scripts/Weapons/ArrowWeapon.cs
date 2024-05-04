using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ArrowWeapon : WeaponBase
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform wpn_Arrow;
    [SerializeField] GameObject arrowOrigin;
    string ARROW_SPAWN = "arrow_Origin";
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
        weaponPosition = GetComponentInParent<WeaponPosition>();
        //transform.rotation = weaponPosition.playerRotation;
        
        //arrowOrigin.transform.SetPositionAndRotation(transform.position, weaponPosition.playerRotation);
        GameObject arrowSpawn = Instantiate(arrowOrigin);
        arrowSpawn.transform.position = transform.position;
        
        //Transform originTransform = arrowOrigin.transform;

        for (int i = 0; i < weaponStats.amount; i++)
        {
            
            //float angle = i * ((Mathf.PI*2)/4) / weaponStats.amount;
            float angle = ((weaponStats.coneAngle * Mathf.Deg2Rad) / weaponStats.amount) * i;
            float angleOffset = (weaponStats.coneAngle / weaponStats.amount) * i;
            float x = Mathf.Cos(angle) * weaponStats.coneRange;
            float z = Mathf.Sin(angle) * weaponStats.coneRange;
            float angleDegrees = 90 - angleOffset;
            Vector3 pos = transform.position + new Vector3(x, 1, z);
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject arrowObject = Instantiate(arrowPrefab, pos, rot, arrowSpawn.transform);

            ArrowCollider arrowCollider = arrowObject.GetComponent<ArrowCollider>();
            arrowCollider.damage = weaponStats.damage;
            arrowCollider.pierce = weaponStats.pierce;
            arrowCollider.speed = weaponStats.speed;

            //arcStart -= increment;
        }
        //arrowSpawn.transform.SetPositionAndRotation(transform.position, weaponPosition.playerRotation);
        Quaternion pRotation = weaponPosition.playerRotation;
        float pRotationY = pRotation.y;
        arrowSpawn.transform.rotation = weaponPosition.playerRotation * Quaternion.AngleAxis(-90, Vector3.up);
        //arrowSpawn.transform.rotation = Quaternion.AngleAxis(-90 * pRotationY, Vector3.up);
        
    }
}
