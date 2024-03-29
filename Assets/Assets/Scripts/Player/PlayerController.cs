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
    [SerializeField] private TrailRenderer trail;

    public float health = 100;

    private Vector2 InputVector;
    private Vector3 MousePosition;

    private int canDash = 1;
    private bool dashing;
    private float dashSpeed = 7.5f;
    private float dashTime = 0.1f;
    private float dashCooldown = 2.5f;

    private void Start()
    {

    }
    private void Update()
    {
        if (dashing)
            return;

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        InputVector = new Vector2(h, v);

        MousePosition = Input.mousePosition;

        var targetVector = new Vector3(InputVector.x, 0, InputVector.y);

        if (Input.GetKeyDown(KeyCode.Space) && canDash > 0)
        {
            StartCoroutine(Dash(targetVector));
        }

        MovePlayer(targetVector);
        MouseRotate();
    }

    private void MouseRotate()
    {
        Ray ray = camera.ScreenPointToRay(MousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }

    }

    private void MovePlayer(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
    }

    private IEnumerator Dash(Vector3 targetVector)
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
        Debug.Log("Can Dash");
        canDash++;
    }
    public void TakeDamage(float value)
    {
        health -= value;
        Debug.Log(health);
        if(health <= 0)
        {
            Debug.Log("Death");
        }
    }
}