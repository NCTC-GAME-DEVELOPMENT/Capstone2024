using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoWeapon : WeaponBase
{
    [SerializeField] GameObject yoyoPrefab;
    [SerializeField] Transform wpn_Yoyo;

    //PlayerController playerController;
    //Quaternion playerRot;

    //void Start()
    //{
    //MC = GetComponentInChildren(MeshCollider);
    //}
    public override void Update()
    {
        base.Update();
        //playerController = GetComponent<PlayerController>();
        //playerRot = playerController.transform.rotation;
        //transform.Rotate(Vector3.up, 0, Space.World);
        transform.Rotate(Vector3.up, weaponStats.speed * Time.deltaTime, Space.Self);
        
    }
    public override void Attack()
    {
        //scale up to attack size to go out from player
        for (int i = 0; i < weaponStats.amount; i++)
        {
            

            float angle = i * Mathf.PI * 2 / weaponStats.amount;
            float x = Mathf.Cos(angle) * weaponStats.coneRange;
            float z = Mathf.Sin(angle) * weaponStats.coneRange;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject yoyoObject = Instantiate(yoyoPrefab, pos, rot, wpn_Yoyo);            

            YoyoCollider yoyoCollider = yoyoObject.GetComponent<YoyoCollider>();
            yoyoCollider.damage = weaponStats.damage;
            yoyoCollider.attackDuration = weaponStats.attackDuration;
        
        
        }
        
        

        
    }


}
