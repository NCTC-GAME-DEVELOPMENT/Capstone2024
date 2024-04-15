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
    private float projectileInterval = 1f;
    private float projectileSpeed = 10f;

    protected override void InitializeObject()
    {
        base.InitializeObject();
        think = Chase;
    }

    void Chase()
    {
        MoveToPlayer();
        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer <= 12.5)
        {
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
            think = Attack;
            animator.SetTrigger("isAttacking");
        }
    }

    void Attack()
    {
        Vector3 lookAtDirection = playerObj.transform.position - transform.position;
        lookAtDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (Time.time >= projectileCooldown)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            TestProjectile projectileScript = projectile.GetComponent<TestProjectile>();
            projectileScript.damage = damage;

            Vector3 direction = (playerObj.transform.position - projectileSpawnPoint.position).normalized;
            direction.y = 0f;
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.velocity = direction * projectileSpeed;

            projectileCooldown = Time.time + projectileInterval;
        }
        if (distanceToPlayer >= 17.5)
        {
            think = Chase;
            animator.SetTrigger("isMoving");
        }
    }
}
