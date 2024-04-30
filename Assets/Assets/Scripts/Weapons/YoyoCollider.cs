using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoCollider : MonoBehaviour
{
    public int damage;
    public float attackDuration;
    

    private void Update()
    {
        attackDuration -= Time.deltaTime;
        if (attackDuration <= 0f)
        {
            Destroy(gameObject);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("boss"))
        {
            Debug.Log("hit:" + other.gameObject.name);
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            enemyBase.TakeDamage(damage);
        }
    }
}
