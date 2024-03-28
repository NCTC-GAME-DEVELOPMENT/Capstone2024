using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    private Camera mainCamera;
    private float delay = 1f;
    private float damage = 5f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!VisibleToCamera())
            Destroy(gameObject, delay);
    }

    bool VisibleToCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Renderer>().bounds))
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
