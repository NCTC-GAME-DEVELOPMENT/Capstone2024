using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : WeaponBase
{
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] Transform wpn_Shield;

    WeaponPosition weaponPosition;
    

    public override void Update()
    {
        base.Update();
        //transform.rotation = weaponPosition.GetPlayerRotation();

    }

    public override void Attack()
    {

        float sheildOffset;
        Vector3 pos;
        for (int i = 0; i < weaponStats.amount; i++) 
        {
            sheildOffset = (2 * i) - (2 * (weaponStats.amount/2));
            pos = transform.position + new Vector3(sheildOffset, 0, weaponStats.coneRange);
            Quaternion rot = Quaternion.Euler(0,0,0);
            GameObject shieldObject = Instantiate(shieldPrefab, pos, rot, wpn_Shield);

            ShieldCollider shieldCollider = shieldObject.GetComponent<ShieldCollider>();
            shieldCollider.damage = weaponStats.damage;
            shieldCollider.attackDuration = weaponStats.attackDuration;
        }

        for (int i = 0; i < weaponStats.amount; i++)
        {
            sheildOffset = (2 * i) - (2 * (weaponStats.amount / 2));
            pos = transform.position + new Vector3(sheildOffset, 0, -weaponStats.coneRange);
            Quaternion rot = Quaternion.Euler(0, 180, 0);
            GameObject shieldObject = Instantiate(shieldPrefab, pos, rot, wpn_Shield);

            ShieldCollider shieldCollider = shieldObject.GetComponent<ShieldCollider>();
            shieldCollider.damage = weaponStats.damage;
            shieldCollider.attackDuration = weaponStats.attackDuration;
        }

        for (int i = 0; i < weaponStats.amount; i++)
        {
            sheildOffset = (2 * i) - (2 * (weaponStats.amount / 2));
            pos = transform.position + new Vector3(weaponStats.coneRange, 0, sheildOffset);
            Quaternion rot = Quaternion.Euler(0, 90, 0);
            GameObject shieldObject = Instantiate(shieldPrefab, pos, rot, wpn_Shield);

            ShieldCollider shieldCollider = shieldObject.GetComponent<ShieldCollider>();
            shieldCollider.damage = weaponStats.damage;
            shieldCollider.attackDuration = weaponStats.attackDuration;
        }

        for (int i = 0; i < weaponStats.amount; i++)
        {
            sheildOffset = (2 * i) - (2 * (weaponStats.amount / 2));
            pos = transform.position + new Vector3(-weaponStats.coneRange, 0, sheildOffset);
            Quaternion rot = Quaternion.Euler(0, 270, 0);
            GameObject shieldObject = Instantiate(shieldPrefab, pos, rot, wpn_Shield);

            ShieldCollider shieldCollider = shieldObject.GetComponent<ShieldCollider>();
            shieldCollider.damage = weaponStats.damage;
            shieldCollider.attackDuration = weaponStats.attackDuration;
        }
    }


}
