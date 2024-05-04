using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOrigin : MonoBehaviour
{
    // Start is called before the first frame update
    Transform firstArrow;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0) Destroy(gameObject);
        firstArrow = transform.GetChild(1);
        float arrowDistance = Vector3.Distance(firstArrow.position, transform.position);
        if (arrowDistance > 200f) Destroy(gameObject);
    }
}
