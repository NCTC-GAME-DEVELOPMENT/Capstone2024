using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public float gameSpeed = 2.0f;

    public void Awake()
    {
        UnPauseGame(); 
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = gameSpeed;
    }
}
