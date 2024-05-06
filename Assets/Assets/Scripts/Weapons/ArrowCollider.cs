using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour
{
    public int damage;
    public int pierce;
    int hitcount = 0;
    public float speed;
    public float attackDuration;
    Vector3 spawnPoint;
    float shotDistance;

    void Start()
    {
        spawnPoint = transform.position;
    }
    void Update()
    {
        transform.Translate((Vector3.forward * Time.deltaTime) * speed);
        shotDistance = Vector3.Distance(transform.position, spawnPoint);
        //attackDuration -= Time.deltaTime;
        if (shotDistance >= 50)
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
            hitcount++;
            if (hitcount >= pierce) Destroy(gameObject);
        }
    }
}
