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
    public float attackTime;
    public float attackTimer;
    public int damage;
    private bool attackAnim = false;
    private float idleTimer = 0.375f;

    void Start()
    {
        playerController = animator.GetComponent<PlayerController>();
        MR = gameObject.GetComponent<MeshRenderer>();
        MC = gameObject.GetComponent<MeshCollider>();
        MC.enabled = false;
        MR.enabled = false;
        attackTimer = attackTime;
        damage = 5;
        attackTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            if (!playerController.dead)
                animator.SetTrigger("isAttacking");
            attackAnim = true;
            Attack();
        }
        if (attackAnim)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                idleTimer = 0.375f;
                attackAnim = false;
                animator.SetTrigger("isIdle");
            }
        }
    }

    public void Attack()
    {
        if (attackTimer > 0)
            return;

        MR.enabled = true;
        MC.enabled = true;
        attackTimer = attackTime;
        StartCoroutine(Delay(delay));
    }

    public void DisableAttack()
    {
        MR.enabled = false;
        MC.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("enemy"))
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
}
