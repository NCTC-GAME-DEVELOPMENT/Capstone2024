using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSound : MonoBehaviour
{
    public AudioClip soundClip;
    public AudioClip[] soundlist; 

    AudioSource source; 


    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySound(); 
        }

    }

    void PlaySound()
    {
        soundClip = soundlist[Random.Range(0, 5)]; 
        source.volume = Random.Range(.7f, 1.1f);
        source.pitch = Random.Range(.8f, 1f); 

        source.PlayOneShot(soundClip); 
    }
}
