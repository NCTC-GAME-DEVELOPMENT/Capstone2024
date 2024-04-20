using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;

    [SerializeField] WeaponSO startingWeapon;

    private void Start()
    {
        AddWeapon(startingWeapon);
    }
    public void AddWeapon(WeaponSO weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.prefab, weaponObjectsContainer);

        weaponGameObject.GetComponent<WeaponBase>().SetData(weaponData);
        Level level = GetComponent<Level>();
        if (level != null)
        {
            level.AddAvailableUpgrades(weaponData.upgrades);
        }
    }
}
