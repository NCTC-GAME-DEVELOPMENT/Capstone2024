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
    //[SerializeField] private TrailRenderer trail;
    public Animator animator;
    
    Timer timer;

    public int health = 100;

    private Vector2 inputVector;
    private Vector3 mousePosition;

    //private int canDash = 2;
    //private bool dashing;
    //private float dashSpeed = 7.5f;
    //private float dashTime = 0.1f;
    //private float dashCooldown = 2.5f;

    private void Start()
    {
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
        animator.SetBool("isMoving", isMoving);
        
        var targetVector = new Vector3(inputVector.x, 0, inputVector.y);

        //if (dashing)
            //return;

        /*if (Input.GetKeyDown(KeyCode.Space) && canDash > 0)
        {
            StartCoroutine(Dash(targetVector));
        }*/

        MovePlayer(targetVector);
        MouseRotate();
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

    /*private IEnumerator Dash(Vector3 targetVector)
    {
        canDash--;
        dashing = true;
        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * dashSpeed;
        transform.position = targetPosition;
        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        Debug.Log("Dash Charge");
        canDash++;
    }*/
    public void TakeDamage(int value)
    {
        health -= value;
        timer.HardModeAdjust(false);
        Debug.Log("Player Health Is " + health);
        if(health <= 0)
        {
            Debug.Log("Death");
        }
    }
}