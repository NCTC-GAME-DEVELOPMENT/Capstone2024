using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;

    [SerializeField] WeaponSO startingWeapon;

    [SerializeField] Transform player;

    private void Start()
    {
        AddWeapon(startingWeapon);
    }

    private void Update()
    {
        
        transform.position = player.transform.position;
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

    public virtual Quaternion GetPlayerRotation()
    {
        Quaternion playerRotation = player.transform.rotation;
        return playerRotation;
    }
}
