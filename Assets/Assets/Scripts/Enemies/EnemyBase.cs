using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject playerObj;
    protected Transform playerTransform;
    protected PlayerController playerController;

    protected bool contact = false;
    protected float contactCooldown;
    private float contactInterval = 1f;
    public float contactDamage = 2.5f;

    public EnemyStatsRow[] StatsChart; 

    protected delegate void ThinkFunction();
    protected ThinkFunction think;

    protected float health;



    // Start is called before the first frame update
    private void Start()
    {
        InitializeObject();
    }

    protected virtual void InitializeObject()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerController = FindObjectOfType<PlayerController>();
        playerTransform = playerController.transform;
        think = MoveToPlayer;
    }

    // Update is called once per frame
    private void Update()
    {
        ContactDamage();
        think?.Invoke();
    }

    protected virtual void MoveToPlayer()
    {
        navMeshAgent.SetDestination(playerTransform.position);
    }

    protected virtual void ContactDamage()
    {
        if (contact && Time.time >= contactCooldown)
        {
            playerController.TakeDamage(contactDamage);
            contactCooldown = Time.time + contactInterval;
        }
    }    

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Crawler is at " + health + "HP");
        if (health <= 0)
            Death();
    }
    
    public virtual void Death()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            contact = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            contact = false;
    }
}
