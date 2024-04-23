using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]

public class WeaponStats
{
    public int damage;
    public int amount;
    public float speed;
    public int pierce;
    public float cooldownDuration;
    public float attackDuration;
    public float coneAngle;
    public float coneRange;

    public WeaponStats(int damage, int amount, float speed, int pierce, float cooldownDuration, float attackDuration, float coneAngle, float coneRange)
    {
        this.damage = damage;
        this.amount = amount;
        this.speed = speed;
        this.pierce = pierce;
        this.cooldownDuration = cooldownDuration;
        this.attackDuration = attackDuration;
        this.coneAngle = coneAngle;
        this.coneRange = coneRange;
    }

    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.amount += weaponUpgradeStats.amount;
        this.speed += weaponUpgradeStats.speed;
        this.pierce += weaponUpgradeStats.pierce;
        this.cooldownDuration += weaponUpgradeStats.cooldownDuration;
        this.attackDuration += weaponUpgradeStats.attackDuration;
        this.coneAngle += weaponUpgradeStats.coneAngle;
        this.coneRange += weaponUpgradeStats.coneRange;
    }
}


[CreateAssetMenu(fileName = "WeaponSO", menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject prefab;
    public List<UpgradeData> upgrades;
}
