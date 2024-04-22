using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SorceressAttack : MonoBehaviour
{
    private Sorceress sorceress;
    private PlayerController playerController;
    private MeshRenderer MR;
    private int damage;
    private bool isPlayerInRange;
    private float warningTime = 2f;
    private float activeTime = 0.1f;
    private bool attacked = false;
    // Start is called before the first frame update
    private void Start()
    {
        sorceress = FindObjectOfType<Sorceress>();
        damage = sorceress.damage;
        playerController = FindObjectOfType<PlayerController>();
        MR = GetComponent<MeshRenderer>();
        Material warningMaterial = MR.material;
        Color currentColor = warningMaterial.color;
        currentColor.a = 0.5f;
        warningMaterial.color = currentColor;
    }

    public void Update()
    {
        warningTime -= Time.deltaTime;
        if (warningTime <= 0)
        {
            Material warningMaterial = MR.material;
            Color currentColor = warningMaterial.color;
            currentColor.a = 1f;
            warningMaterial.color = currentColor;
            if (isPlayerInRange)
            {
                if (!attacked)
                {
                    attacked = true;
                    playerController.TakeDamage(damage);
                }

            }
            activeTime -= Time.deltaTime;
            if (activeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
