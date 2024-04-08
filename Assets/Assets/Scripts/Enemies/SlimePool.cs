using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour
{
    private PlayerController playerController;

    private bool contact = false;
    private int contactDamage;
    private float contactCooldown;
    private float contactInterval = 1f;
    private int StatsChartRow;

    public EnemyStatsRow[] StatsChart;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        Timer gameManager = FindObjectOfType<Timer>();
        StatsChartRow = gameManager.StatsChartRow;
        contactDamage = StatsChart[StatsChartRow].contactDamage;
    }

    private void Update()
    {
        ContactDamage();
    }
    protected virtual void ContactDamage()
    {
        if (contact && Time.time >= contactCooldown)
        {
            playerController.TakeDamage(contactDamage);
            contactCooldown = Time.time + contactInterval;
        }
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
