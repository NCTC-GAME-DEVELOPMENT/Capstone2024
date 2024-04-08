using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeTest : MonoBehaviour
{
    public Transform pivot;

    public bool HideTrigger = false;

    public float delay = 0.1f;
    public float attackTime;
    public float timer;
    public int damage;

    public MeshRenderer MR;
    public MeshCollider MC;

    void Start()
    {
        MR = gameObject.GetComponent<MeshRenderer>();
        MC = gameObject.GetComponent<MeshCollider>();
        MC.enabled = false;
        MR.enabled = false;
        timer = attackTime;
        damage = 5;
        attackTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (timer > 0)
            return;

        MR.enabled = true;
        MC.enabled = true;
        timer = attackTime;
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
