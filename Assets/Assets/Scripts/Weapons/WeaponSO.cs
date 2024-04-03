using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class WeaponStats
{
    public float damage;
    public float speed;
    public int pierce;
    public float cooldownDuration;
    public float coneAngle;
    public float coneRange;

    public WeaponStats(float damage, float speed, int pierce, float cooldownDuration, float coneAngle, float coneRange)
    {
        this.damage = damage;
        this.speed = speed;
        this.pierce = pierce;
        this.cooldownDuration = cooldownDuration;
        this.coneAngle = coneAngle;
        this.coneRange = coneRange;
    }
}


[CreateAssetMenu(fileName = "WeaponSO", menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject prefab;

}
