using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    protected GameObject playerObj;
    protected Transform playerTransform;
    protected PlayerController playerController;

    protected bool contact = false;
    protected float contactCooldown;
    public float contactInterval = 1f;
    public float contactDamage = 2.5f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.FindWithTag("Player");
        playerTransform = playerObj.transform;
        playerController = playerObj.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (contact && Time.time >= contactCooldown)
        {
            playerController.TakeDamage(contactDamage);
            contactCooldown = Time.time + contactInterval;
        }
    }

    protected virtual void MoveToPlayer()
    {
        navMeshAgent.SetDestination(playerTransform.position);
    }

    public virtual void TakeDamage(float damage)
    {
        //TAKE DAMAGE
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
