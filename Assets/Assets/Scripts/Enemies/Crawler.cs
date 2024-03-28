using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crawler : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    private GameObject playerObj;
    private Transform playerTransform;
    private PlayerController playerController;

    private bool contact = false;
    private float contactCooldown;
    private float contactInterval = 1f;
    private float damage = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.FindWithTag("Player");
        playerTransform = playerObj.transform;
        playerController = playerObj.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(playerTransform.position);
        if (contact && Time.time >= contactCooldown)
        {
            playerController.TakeDamage(damage);
            contactCooldown = Time.time + contactInterval;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            contact = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            contact = false;
    }
}
