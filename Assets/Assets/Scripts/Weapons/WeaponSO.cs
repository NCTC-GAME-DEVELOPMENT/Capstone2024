using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    public GameObject prefab;
    public float damage;
    public float speed;
    public int pierce;
    public float cooldownDuration;
    public float coneAngle;
    public float coneRange;
}
