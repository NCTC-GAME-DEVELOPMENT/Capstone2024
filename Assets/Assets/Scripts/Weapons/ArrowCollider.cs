using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour
{
    public int damage;
    public int pierce;
    int hitcount = 0;

    //void Update()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("boss"))
        {
            Debug.Log("hit:" + other.gameObject.name);
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            enemyBase.TakeDamage(damage);
            hitcount++;
            if (hitcount >= pierce) Destroy(gameObject);
        }
    }
}
