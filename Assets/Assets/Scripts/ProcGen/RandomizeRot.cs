using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRot : MonoBehaviour
{
    
    void Start()
    {
        RandomizeYRot();
    }

    void RandomizeYRot()
    {
        Quaternion randYRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
        transform.rotation = randYRot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
