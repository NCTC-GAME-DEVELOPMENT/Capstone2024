using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] ExperienceBar experienceBar;
    private PlayerMeleeTest playerMelee;
    private Level levelUp;
    public Animator animator;
    Timer timer;
    private Rigidbody rb;

    public int baseHealth = 100;
    public int maxHealth;
    public int currentHealth;
    public int flatDR = 0;
    public float percentDR = 0;
    public int baseDamage = 5;
    public int damage;
    public float baseAttackSpeed = 2f;
    public float baseAttackSpeedRatio = 0f;
    public float attackSize = 1;
    public float moveSpeed = 5f;

    public int xp = 0;
    public int nextLevelXP = 1;
    private int prevLevel = 1;
    public int level = 1;
    
    public bool dead = false;

    private Vector2 inputVector;
    private Vector3 mousePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        animator = GetComponent<Animator>();
        timer = FindObjectOfType<Timer>();
        playerMelee = FindObjectOfType<PlayerMeleeTest>();
        levelUp = GetComponent<Level>();

        maxHealth = baseHealth;
        currentHealth = maxHealth;
        damage = baseDamage;

        experienceBar.UpdateExperienceSlider(xp, nextLevelXP);
    }
    private void Update()
    {
        experienceBar.UpdateExperienceSlider(xp, nextLevelXP);
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        inputVector = new Vector2(h, v);

        mousePosition = Input.mousePosition;

        bool isMoving = inputVector.magnitude > 0;
        
        
        var targetVector = new Vector3(inputVector.x, 0, inputVector.y);

        if(Input.GetKeyDown(KeyCode.B))
        {
            playerMelee.UpdateStats();
        }

        if (!dead)
        {
            MovePlayer(targetVector);
            MouseRotate();
            animator.SetBool("isMoving", isMoving);
            if (xp >= nextLevelXP) //If you were to level up twice with one XP gem it only gives the selection popup once
            {                      //This shouldn't be an issue since it shouldn't be possible to have that happen past the first handful of levels.
                levelUp.LevelUp();
                UpdateStats();
                xp -= nextLevelXP;
                nextLevelXP += nextLevelXP + 1;
                level++;
            }

        }
    }

    private void MouseRotate()
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        LayerMask groundLayerMask = LayerMask.GetMask("Ground");
        if(Physics.Raycast(ray,out RaycastHit hitInfo, maxDistance: 300f, groundLayerMask))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void MovePlayer(Vector3 targetVector)
    {
        targetVector.Normalize();

        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;

        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
    }

    public void UpdateStats()
    {
        maxHealth += 5;
        currentHealth += 5;
        flatDR += 1;
        percentDR += 1;
        damage += 5;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        playerMelee.damage = damage;
    }
    public void TakeDamage(int value)
    {
        float reduction = (value * (percentDR / 100f));
        if (reduction < 1) //We cannot have the player getting that +1 HP every time, we're mean.
        {
            int roundedReduction = Mathf.CeilToInt(reduction);
            reduction = roundedReduction;
        }
        int adjustedDamage = value - (int)reduction;
        adjustedDamage -= flatDR;
        currentHealth -= adjustedDamage;
        timer.HardModeAdjust(false);
        Debug.Log("Player Health Is " + currentHealth);
        if(currentHealth <= 0 && !dead)
        {
            dead = true;
            animator.SetTrigger("Death");
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Death");
        }
    }
}