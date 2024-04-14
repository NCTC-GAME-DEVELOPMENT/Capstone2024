using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject playerObj;
    protected PlayerController playerController;
    protected Timer timer;
    protected Animator animator;

    protected bool boss = false;
    protected int health;
    public int damage;
    protected int contactDamage;
    protected bool contact = false;
    protected float contactCooldown;
    private float contactInterval = 1f;

    protected int StatsChartRow;
    public EnemyStatsRow[] StatsChart; 

    protected delegate void ThinkFunction();
    protected ThinkFunction think;

    // Start is called before the first frame update
    private void Start()
    {
        InitializeObject();
    }

    protected virtual void InitializeObject()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = FindObjectOfType<Timer>();
        animator = GetComponent<Animator>();
        StatsChartRow = timer.StatsChartRow;
        playerController = FindObjectOfType<PlayerController>();
        playerObj = FindObjectOfType<PlayerController>().gameObject;
        health = StatsChart[StatsChartRow].health;
        contactDamage = StatsChart[StatsChartRow].contactDamage;
        damage = StatsChart[StatsChartRow].damage;
        think = MoveToPlayer;
    }

    // Update is called once per frame
    private void Update()
    {
        ContactDamage();
        think?.Invoke();
        if (timer.currentTime <= 1f && !boss)
            Death();
    }

    protected virtual void MoveToPlayer()
    {
        navMeshAgent.SetDestination(playerObj.transform.position);
    }

    protected virtual void ContactDamage()
    {
        if (contact && Time.time >= contactCooldown)
        {
            playerController.TakeDamage(contactDamage);
            contactCooldown = Time.time + contactInterval;
        }
    }    

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("enemy is at " + health + "HP");
        if (health <= 0)
            Death();
    }
    
    protected virtual void Death()
    {
        Destroy(gameObject);
        timer.HardModeAdjust(true);
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
