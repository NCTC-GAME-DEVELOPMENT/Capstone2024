using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeTest : MonoBehaviour
{
    public Transform pivot;

    public bool HideTrigger = false;

    public float delay = 0.1f;
    public float attackTime = 1f;
    public float timer;
    public float damage = 5;

    public MeshRenderer MR;
    public BoxCollider BC;

    void Start()
    {
        MR = gameObject.GetComponent<MeshRenderer>();
        BC = gameObject.GetComponent<BoxCollider>();
        BC.enabled = false;
        MR.enabled = false;
        timer = attackTime;
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
        {
            return;
            // Already Making an Attack 
        }
        MR.enabled = true;
        BC.enabled = true;
        timer = attackTime;
        StartCoroutine(Delay(delay));
    }

    public void DisableAttack()
    {
        MR.enabled = false;
        BC.enabled = false;
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
