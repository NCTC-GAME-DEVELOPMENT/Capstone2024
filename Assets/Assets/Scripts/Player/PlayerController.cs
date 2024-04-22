using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private float moveSpeed = 5;
    public int xp = 0;
    public int nextLevelXP = 1;
    public int level = 1;
    //[SerializeField] private TrailRenderer trail;
    public Animator animator;
    
    Timer timer;
    private Rigidbody rb;

    public int health = 100;
    public bool dead = false;

    private Vector2 inputVector;
    private Vector3 mousePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        animator = GetComponent<Animator>();
        timer = FindObjectOfType<Timer>();
        //trail = GetComponent<TrailRenderer>();
    }
    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        inputVector = new Vector2(h, v);

        mousePosition = Input.mousePosition;

        bool isMoving = inputVector.magnitude > 0;
        
        
        var targetVector = new Vector3(inputVector.x, 0, inputVector.y);

        if (!dead)
        {
            MovePlayer(targetVector);
            MouseRotate();
            animator.SetBool("isMoving", isMoving);
            if (xp >= nextLevelXP)
            {
                Debug.Log("LEVEL UP");
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
    public void TakeDamage(int value)
    {
        health -= value;
        timer.HardModeAdjust(false);
        Debug.Log("Player Health Is " + health);
        if(health <= 0 && !dead)
        {
            dead = true;
            animator.SetTrigger("Death");
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Death");
        }
    }
}