using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBarrier : MonoBehaviour
{
    public GameObject[] barriersToPickFrom;

    void Start()
    {
        PickItemB();
    }

    void PickItemB()
    {
        int randomIndex = Random.Range(0, barriersToPickFrom.Length);
        GameObject clone = Instantiate(barriersToPickFrom[randomIndex], transform.position, Quaternion.identity);
    }
}
