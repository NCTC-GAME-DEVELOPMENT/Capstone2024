using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAreaSpawner : MonoBehaviour
{
    public GameObject debrisToSpread;
    public GameObject barriersToSpread;

    public int numDebrisToSpawn = 10;
    public int numBarriersToSpawn = 5;

    public float itemXSpread = 20;
    public float itemYSpread = 0;
    public float itemZSpread = 20;

    void Start()
    {
        for (int i = 0; i < numDebrisToSpawn; i++)
        {
            SpreadDebris();
        }

        for(int i = 0; i < numBarriersToSpawn; i++)
        {
            SpreadBarriers();
        }
    }

    void SpreadDebris()
    {
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        GameObject clone = Instantiate(debrisToSpread, randPosition, Quaternion.identity);
    }

    void SpreadBarriers()
    {
        Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
        GameObject clone = Instantiate(barriersToSpread, randPosition, Quaternion.identity);
    }
}
