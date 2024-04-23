using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    [SerializeField] Transform player;
    

    // Update is called once per frame
    public void Update()
    {

        transform.position = player.transform.position;
    }

    public virtual Quaternion GetPlayerRotation()
    {
        Quaternion playerRotation = player.transform.rotation;
        return playerRotation;
    }
}
