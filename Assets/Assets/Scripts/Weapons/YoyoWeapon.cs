using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoWeapon : WeaponBase
{
    [SerializeField] GameObject yoyoPrefab;
    [SerializeField] Transform wpn_Yoyo;



    //void Start()
    //{
    //MC = GetComponentInChildren(MeshCollider);
    //}
    public override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.up, weaponData.stats.speed * Time.deltaTime);
    }
    public override void Attack()
    {
        //scale up to attack size to go out from player
        for (int i = 0; i < weaponData.stats.amount; i++)
        {
            

            float angle = i * Mathf.PI * 2 / weaponData.stats.amount;
            float x = Mathf.Cos(angle) * weaponData.stats.coneRange;
            float z = Mathf.Sin(angle) * weaponData.stats.coneRange;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject yoyoObject = Instantiate(yoyoPrefab, pos, rot, wpn_Yoyo);            

            YoyoCollider yoyoCollider = yoyoObject.GetComponent<YoyoCollider>();
            yoyoCollider.damage = weaponData.stats.damage;
            yoyoCollider.attackDuration = weaponData.stats.attackDuration;
        
        
        }
        
        

        
    }


}
