using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sorceress : EnemyBase
{
    [SerializeField] private Transform[] summonPoints;
    [SerializeField] private GameObject enemySummon;
    [SerializeField] private GameObject warning;
    private GameObject playArea;
    private float teleportDistance = 15f;
    private float rotationSpeed = 200f;

    private bool inPlayArea = false;
    private float cooldownTimer = 2f;

    // Start is called before the first frame update
    protected override void InitializeObject()
    {
        base.InitializeObject();
        boss = true;
        playArea = GameObject.FindGameObjectWithTag("playArea");
        animator.SetTrigger("isMoving");
        think = Chase;
    }
    void Chase()
    {
        LookAtPlayer(rotationSpeed);
        MoveToPlayer();
        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer <= 12.5 && inPlayArea)
        {
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
            int choice = Random.Range(1, 3);
            GameObject[] bruisers = GameObject.FindGameObjectsWithTag("enemy");
            Debug.Log(bruisers.Length);
            if (choice == 1 && bruisers.Length <= 1)
            {
                Summon();
            }
            else
            {
                Attack();
            }
            if (choice == 2)
            {
                Attack();
            }
            animator.SetTrigger("isAttacking");
            think = Teleport;
        }
    }
    private void Attack()
    {
        Vector3 spawnPosition = playerObj.transform.position + Vector3.up * 0.25f;
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        Instantiate(warning, spawnPosition, rotation);
        Vector3 directionToPlayer = playerObj.transform.position - transform.position;
        directionToPlayer.y = 0f;

        transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }
    private void Summon()
    {
        foreach (Transform summonPoint in summonPoints)
        {
            GameObject summon = Instantiate(enemySummon, summonPoint.position, summonPoint.rotation);
            summon.GetComponent<EnemyBase>().boss = true;
        }

    }
    private void Teleport()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            Vector3 randomDirection = Random.onUnitSphere * teleportDistance;
            randomDirection += playerObj.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, teleportDistance, NavMesh.AllAreas);

            if (playArea.GetComponent<Collider>().bounds.Contains(hit.position))
            {
                navMeshAgent.Warp(hit.position);
                think = Idle;
                cooldownTimer = 1f;
                animator.SetTrigger("isIdle");
                Vector3 directionToPlayer = playerObj.transform.position - transform.position;
                directionToPlayer.y = 0f;

                transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
            else
            {
                Teleport();
            }
        }
    }

    private void Idle()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            animator.SetTrigger("isMoving");
            think = Chase;
            cooldownTimer = 2f;
        }
    }
    void LookAtPlayer(float rotationSpeed)
    {
        Vector3 lookAtDirection = playerObj.transform.position - transform.position;
        lookAtDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("playArea"))
            inPlayArea = true;
    }
}
