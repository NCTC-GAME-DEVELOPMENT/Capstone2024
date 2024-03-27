using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerControl : MonoBehaviour
{
    public bool HideTrigger = false; 
    public float AttackTime = .2f;
    public float timer = 0; 

    void Start()
    {
        if (HideTrigger)
        {
            MeshRenderer MR = gameObject.GetComponent<MeshRenderer>();
            MR.enabled = false; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; 
        if (timer < 0)
        {
            gameObject.SetActive(false);
            Debug.Log("Attack End");
        }
        

    }

    public void MakeAttack()
    {
        Debug.Log("Attack Start");
        if (timer > 0)
        {
            return; 
            // Already Making an Attack 
        }
        gameObject.SetActive(true);
        timer = AttackTime; 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit:" + other.gameObject.name); 
    }

}
