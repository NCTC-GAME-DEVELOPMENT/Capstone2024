using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events; 

public class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected GameObject playerObj;
    protected PlayerController playerController;
    protected Timer timer;
    protected Animator animator;
    protected Rigidbody rb;
    protected ColorChange colorChange;
    protected AudioHandler audioHandler;
    [SerializeField] protected AudioClip damageSound;
    [SerializeField] protected GameObject[] xpPrefab;

    public bool boss = false;
    protected bool inPlayArea = false;
    protected int health;
    public int damage;
    protected int contactDamage;
    protected bool contact = false;
    protected float contactCooldown;
    private float contactInterval = 1f;

    protected int StatsChartRow;
    protected int xpLevel;
    public EnemyStatsRow[] StatsChart; 

    protected delegate void ThinkFunction();
    protected ThinkFunction think;

    public UnityEvent OnDeath; 

    // Start is called before the first frame update
    private void Start()
    {
        InitializeObject();
    }

    protected virtual void InitializeObject()
    {
        if (GetComponent<Rigidbody>() != null)
            rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = FindObjectOfType<Timer>();
        animator = GetComponent<Animator>();
        StatsChartRow = timer.StatsChartRow;
        xpLevel = timer.xpLevel;
        playerController = FindObjectOfType<PlayerController>();
        playerObj = FindObjectOfType<PlayerController>().gameObject;
        colorChange = GetComponent<ColorChange>();
        audioHandler = playerObj.GetComponent<AudioHandler>();
        health = StatsChart[StatsChartRow].health;
        contactDamage = StatsChart[StatsChartRow].contactDamage;
        damage = StatsChart[StatsChartRow].damage;
        think = MoveToPlayer;
    }

    // Update is called once per frame
    protected virtual void Update()
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
        
        if (inPlayArea)
        {
            audioHandler.EnemyDamageSound(damageSound);
            health -= damage;
            colorChange.ChangeToRed();
            Debug.Log("enemy is at " + health + "HP");
            if (health <= 0)
                Death();
        }
    }
    
    protected virtual void Death()
    {
        OnDeath?.Invoke();      
        timer.HardModeAdjust(true);
        Instantiate(xpPrefab[xpLevel], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            contact = true;
        if (other.gameObject.CompareTag("playArea"))
            inPlayArea = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            contact = false;
    }
}
