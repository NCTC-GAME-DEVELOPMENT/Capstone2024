using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField] private new Camera camera;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    private void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        MovePlayer(targetVector);
        MouseRotate();
    }

    private void MouseRotate()
    {
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);
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
}
