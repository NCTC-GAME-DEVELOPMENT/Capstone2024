using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime = 1200f;
    private float currentTime;
    private bool isTimerRunning = false;

    public TMP_Text timerText;

    public GameObject crawlerPrefab;
    public GameObject rangedPrefab;

    public Transform[] spawnPoints;
    public float spawnTimer = 5f;

    void Start()
    {
        ResetTimer();
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateUIText();

            if (currentTime <= 0f)
            {
                ResetTimer();
                StartTimer();
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        InvokeRepeating("SpawnEnemy", 0f, spawnTimer);
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        CancelInvoke("SpawnEnemy");
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = currentTime >= totalTime * 0.5f ? crawlerPrefab : rangedPrefab;
        // Randomly select a spawn point from the array
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }
}
