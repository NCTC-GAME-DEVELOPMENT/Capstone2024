using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeTest : MonoBehaviour
{
    public MeshRenderer MR;
    public MeshCollider MC;
    public Transform pivot;
    [SerializeField] private Animator animator;
    private PlayerController playerController;

    public bool HideTrigger = false;

    public float delay = 0.1f;
    private float baseAttackTime = 0.375f;
    public float attackTime;
    public float attackTimer;
    private float idleTimer;
    private float newIdleTimer;
    public int damage;
    private bool attackAnim = false;

    void Start()
    {
        playerController = animator.GetComponent<PlayerController>();
        MR = gameObject.GetComponent<MeshRenderer>();
        MC = gameObject.GetComponent<MeshCollider>();
        MC.enabled = false;
        MR.enabled = false;

        idleTimer = playerController.baseAttackSpeed;
        newIdleTimer = playerController.baseAttackSpeed;
        baseAttackTime = 0.375f;
        attackTime = baseAttackTime;

        damage = playerController.damage;
    }

    // Update is called once per frame
    void Update()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0 && !attackAnim)
        {
            if (!playerController.dead)
                animator.SetTrigger("isAttacking");
            attackAnim = true;
            Attack();
        }
        if (attackAnim)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                animator.SetTrigger("isIdle");
                idleTimer = newIdleTimer; //ATTACK SPEED
                attackTimer = attackTime; //ATTACK SPEED
                attackAnim = false;
            }
        }
    }

    public void Attack()
    {
        if (idleTimer > 0)
            return;

        MR.enabled = true;
        MC.enabled = true;
        idleTimer = attackTime;
        StartCoroutine(Delay(delay));
    }

    public void DisableAttack()
    {
        MR.enabled = false;
        MC.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("boss"))
        {
            Debug.Log("hit:" + other.gameObject.name);
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            enemyBase.TakeDamage(damage);
        }
    }
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DisableAttack();
    }
    public void UpdateStats()
    {
        damage = playerController.damage;
        //attackTime -= (attackTime * playerController.baseAttackSpeedRatio);
        //Debug.Log("ATTACK TIME " + attackTime + " | IDLE TIME " + idleTimer);
    }
}
