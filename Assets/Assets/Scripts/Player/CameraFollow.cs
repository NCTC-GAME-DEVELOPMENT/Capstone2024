using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        Vector3 newPosition = target.position + offset;

        transform.position = newPosition;
        transform.LookAt(target);
    }
}
