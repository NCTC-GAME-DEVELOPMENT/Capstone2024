using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPickup : MonoBehaviour
{
    private float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XP"))
        {
            StartCoroutine(PullObject(other.gameObject));
        }
    }
    private IEnumerator PullObject(GameObject xpObject)
    {
        while (xpObject != null)
        {
            // Check if the XP object is still active
            if (!xpObject.activeSelf)
                yield break;

            // Move the XP object towards this object
            Vector3 directionToThisObject = (transform.position - xpObject.transform.position).normalized;
            xpObject.transform.position += directionToThisObject * speed * Time.deltaTime;

            yield return null;
        }
    }
}
