using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponSO weaponData;

    WeaponStats weaponStats;

    float currentCooldown;
    protected virtual void Start()
    {
        currentCooldown = weaponData.stats.cooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.stats.cooldownDuration;
    }

    public virtual void SetData(WeaponSO wd)
    {
        weaponData = wd;


        weaponStats = new WeaponStats
            (wd.stats.damage,
            wd.stats.speed,
            wd.stats.pierce,
            wd.stats.cooldownDuration,
            wd.stats.coneAngle,
            wd.stats.coneRange);
    }
}
