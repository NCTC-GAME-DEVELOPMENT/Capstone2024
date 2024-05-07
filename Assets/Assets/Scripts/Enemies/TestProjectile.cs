using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    private Camera mainCamera;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!VisibleToCamera())
            Destroy(gameObject);
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
