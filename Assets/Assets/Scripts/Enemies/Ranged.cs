using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Ranged : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    NavMeshAgent navMeshAgent;

    private GameObject playerObj;
    private Transform playerTransform;
    private PlayerController playerController;

    private float projectileCooldown;
    private float projectileInterval = 1;
    private float projectileSpeed = 10f;

    private bool contact = false;
    private float contactCooldown;
    private float contactInterval = 1f;

    private float damage = 2.5f;

    public delegate void ThinkFunction();
    ThinkFunction think;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.FindWithTag("Player");
        playerTransform = playerObj.transform;
        playerController = playerObj.GetComponent<PlayerController>();
        think = Chase;
    }

    // Update is called once per frame
    void Update()
    {
        think?.Invoke();
        if (contact && Time.time >= contactCooldown)
        {
            playerController.TakeDamage(damage);
            contactCooldown = Time.time + contactInterval;
        }
    }

    void Chase()
    {
        navMeshAgent.SetDestination(playerTransform.position);
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer <= 12.5)
        {
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
            think = Attack;
        }
    }

    void Attack()
    {
        transform.LookAt(playerTransform.position);
        if (Time.time >= projectileCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (playerTransform.position - projectileSpawnPoint.position).normalized;
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = direction * projectileSpeed;
            projectileCooldown = Time.time + projectileInterval;
        }
    }

    private void OnTriggerEnter(Collider other)
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
