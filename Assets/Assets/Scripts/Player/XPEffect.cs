using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPEffect : MonoBehaviour
{
    private int xpAmount = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().xp += xpAmount;
            Destroy(gameObject);
        }
    }
}
