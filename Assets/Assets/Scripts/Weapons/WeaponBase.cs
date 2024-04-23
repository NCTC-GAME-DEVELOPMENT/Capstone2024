using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponSO weaponData;

    public WeaponStats weaponStats;
    //public float cooldownDuration;
    float timer;
 

    // Update is called once per frame
    public virtual void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Attack();
            timer = weaponData.stats.cooldownDuration;

        }
    }

    public abstract void Attack();


    public virtual void SetData(WeaponSO wd)
    {
        weaponData = wd;


        weaponStats = new WeaponStats
            (wd.stats.damage,
            wd.stats.amount,
            wd.stats.speed,
            wd.stats.pierce,
            wd.stats.cooldownDuration,
            wd.stats.attackDuration,
            wd.stats.coneAngle,
            wd.stats.coneRange);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }
}
