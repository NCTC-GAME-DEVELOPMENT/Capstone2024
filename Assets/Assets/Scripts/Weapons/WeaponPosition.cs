using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    [SerializeField] Transform player;
    public Quaternion playerRotation;

    // Update is called once per frame
    public void Update()
    {

        transform.position = player.transform.position;
        playerRotation = GetPlayerRotation();
    }

    public virtual Quaternion GetPlayerRotation()
    {
        Quaternion playerRotation = player.transform.rotation;
        return playerRotation;
    }
}
