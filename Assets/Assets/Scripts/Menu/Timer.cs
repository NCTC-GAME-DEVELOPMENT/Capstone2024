using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime = 1200f;
    public float currentTime;
    public float nextEvent;
    public int eventCount = 0;
    private bool isTimerRunning = false;
    public TMP_Text timerText;

    public GameObject[] enemyArray;
    public GameObject crawlerPrefab;
    public GameObject slimePrefab;
    public GameObject rangedPrefab;
    public GameObject bruiserPrefab;
    public GameObject bossPrefab;

    public Transform[] spawnPoints;
    public float spawnTimer = 5f;

    public int StatsChartRow = 0;

    public bool hardMode = false;
    public int hardModeLevel = 0;
    public int hardModeReduction = 1;
    public int hardModeGain = 1;
    private float hardModeReductionTimer = 10f;
    private float hardModeRecutionCheck;

    void Start()
    {
        ResetTimer();
        StartTimer();
        enemyArray = new GameObject[1];
        enemyArray[0] = crawlerPrefab;
        hardModeRecutionCheck = hardModeReductionTimer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            currentTime -= 30;
        if (Input.GetKeyDown(KeyCode.Y))
            currentTime += 60;

        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateUIText();

            if (hardMode)
            {
                HardMode();
            }

            if (currentTime <= 1f)
            {
                StopTimer();
            }

            if (currentTime <= nextEvent)
                Event();

        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        InvokeRepeating(nameof(SpawnRandomEnemy), 0f, spawnTimer);
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        CancelInvoke(nameof(SpawnRandomEnemy));
        Event();
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        nextEvent = currentTime - 60;
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

    private void SpawnRandomEnemy()
    {
        GameObject enemyPrefab = enemyArray[Random.Range(0, enemyArray.Length)];

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }

    private void HardMode()
    {
        hardModeRecutionCheck -= Time.deltaTime;
        if (hardModeRecutionCheck <= 0)
        {
            hardModeRecutionCheck = hardModeReductionTimer;
            HardModeAdjust(false);
        }
    }

    public void HardModeAdjust(bool impact)
    {
        if (impact)
            hardModeLevel += hardModeGain;
        if (!impact)
            hardModeLevel -= hardModeReduction;
        if (hardModeLevel <= 0)
            hardModeLevel = 0;
    }
    private void Event()
    {
        StatsChartRow++;
        eventCount++;
        nextEvent = currentTime - 60;
        if (eventCount == 1)
            spawnTimer = 4f;
        if (eventCount == 2)
        {
            enemyArray = new GameObject[3];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = crawlerPrefab;
            enemyArray[2] = slimePrefab;
        }
        if (eventCount == 3)
        {
            enemyArray = new GameObject[2];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
        }
        if (eventCount == 4)
        {
            SpawnEnemy(bruiserPrefab);
        }
        if (eventCount == 5)
        {
            enemyArray = new GameObject[3];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = rangedPrefab;
        }
        if (eventCount == 20)
        {
            SpawnEnemy(bossPrefab);
        }

    }
}
