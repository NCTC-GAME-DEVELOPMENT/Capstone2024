using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPEffect : MonoBehaviour
{
    [SerializeField] private int xpAmount;
    private float rotationSpeed = 100f;
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().xp += xpAmount;
            Destroy(gameObject);
        }
    }
}
