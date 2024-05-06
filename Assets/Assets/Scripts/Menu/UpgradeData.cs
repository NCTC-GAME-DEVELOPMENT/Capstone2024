using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    WeaponUpgrade,
    ItemUpgrade,
    WeaponUnlock,
    ItemUnlock,
    SkillUpgrade,
    SkillUnlock
}

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public Sprite icon;
    public string description;

    public WeaponSO weaponData;
    public WeaponStats weaponUpgradeStats;
    public List<UpgradeData> nextUpgradeData;
    public PlayerStats playerUpgradeStats;


}

[Serializable]
public class PlayerStats
{
    public int baseHealth;
    public int maxHealth;
    public int currentHealth;
    public int healthRegen;
    public float regenTime;
    public int flatDR;
    public float percentDR;
    public int baseDamage;
    public int damage;
    public float baseAttackSpeed;
    public float baseAttackSpeedRatio;
    public float attackSize;
    public float moveSpeed;
}
