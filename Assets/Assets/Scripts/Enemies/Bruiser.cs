using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bruiser : EnemyBase
{
    [SerializeField] private MeshRenderer warningMR;
    [SerializeField] private MeshCollider warningMC;
    [SerializeField] private GameObject warningAngle;
    private EnemyAttack enemyAttack;

    private Vector3 directionToPlayer;
    private float rotationSpeed = 200f;

    private bool windingUp = true;
    private bool attacked = false;
    private float windupTime = 1f;
    public float activeTime = 0.1f;
    private float cooldown = 2f;

    private float attackTimer = 0.625f;
    private bool attackBool = false;

    private bool walkCheck = false;
    private bool faceCheck = false;
    protected override void InitializeObject()
    {
        warningMR.enabled = false;
        warningMC.enabled = false;

        base.InitializeObject();

        enemyAttack = warningAngle.GetComponent<EnemyAttack>();
        enemyAttack.damage = damage;

        think = Chase;
    }

    void Chase()
    {
        if (!walkCheck)
        {
            animator.SetTrigger("isWalking");
            walkCheck = true;
        }
        MoveToPlayer();
        LookAtPlayer(rotationSpeed);

        float distanceToPlayer = Vector3.Distance(playerObj.transform.position, transform.position);
        if (distanceToPlayer <= 4)
        {
            walkCheck = false;
            animator.SetTrigger("isWindingUp");
            navMeshAgent.SetDestination(navMeshAgent.transform.position);
            think = Attack;
        }
    }

    void Attack()
    {
        warningMR.enabled = true;
        warningMC.enabled = true;

        if (windingUp)
        {
            Material warningMaterial = warningMR.material;
            Color currentColor = warningMaterial.color;
            currentColor.a = 0.5f;
            warningMaterial.color = currentColor;

            if (!faceCheck)
            {
                directionToPlayer = playerObj.transform.position - transform.position;
                directionToPlayer.y = 0f;

                faceCheck = true;
            }

            windupTime -= Time.deltaTime;
            if (windupTime <= 0)
            {
                windingUp = false;
                animator.SetTrigger("isAttacking");
            }
        }

        if (!windingUp)
        {
            Material warningMaterial = warningMR.material;
            Color currentColor = warningMaterial.color;
            currentColor.a = 1f;
            warningMaterial.color = currentColor;

            if (enemyAttack.isPlayerInRange && !attacked)
            {
                attacked = true;
                PlayerTakeDamage();
            }

            activeTime -= Time.deltaTime;
            if (activeTime <= 0)
            {
                warningMR.enabled = false;
                warningMC.enabled = false;
                windingUp = true;
                attacked = false;
                faceCheck = false;
                windupTime = 1;
                activeTime = 0.1f;
                rotationSpeed = 200f;
                navMeshAgent.speed = 20f;
                think = Idle;
            }
        }
    }

    void Idle()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && !attackBool)
        {
            LookAtPlayer(rotationSpeed);
            attackBool = true;
            animator.SetTrigger("isIdle");
        }

        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            attackBool = false;
            attackTimer = 0.625f;
            cooldown = 2f;
            navMeshAgent.speed = 4f;
            think = Chase;
        }
    }

    void LookAtPlayer(float rotationSpeed)
    {
        Vector3 lookAtDirection = playerObj.transform.position - transform.position;
        lookAtDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void PlayerTakeDamage()
    {
        playerController.TakeDamage(damage);
    }
}
