using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Ranged : EnemyBase
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private float projectileCooldown;
    private float projectileInterval = 1;
    private float projectileSpeed = 10f;

    public delegate void ThinkFunction();
    ThinkFunction think;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        think = Chase;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        think?.Invoke();
    }

    void Chase()
    {
        MoveToPlayer();
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (distanceToPlayer <= 12.5)
        {
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
            think = Attack;
        }
    }

    void Attack()
    {
        Vector3 lookAtDirection = playerTransform.position - transform.position;
        lookAtDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        if (Time.time >= projectileCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (playerTransform.position - projectileSpawnPoint.position).normalized;
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = direction * projectileSpeed;
            projectileCooldown = Time.time + projectileInterval;
        }
        if (distanceToPlayer >= 17.5)
        {
            think = Chase;
        }
    }
}
