using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickDebris : MonoBehaviour
{
    public GameObject[] debrisToPickFrom;

    void Start()
    {
        PickItemD();
    }

    void PickItemD()
    {
        int randomIndex = Random.Range(0, debrisToPickFrom.Length);
        GameObject clone = Instantiate(debrisToPickFrom[randomIndex], transform.position, Quaternion.identity);
    }
}
