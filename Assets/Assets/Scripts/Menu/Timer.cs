using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public float spawnTimer = 3f;

    public int StatsChartRow = 0;
    public int xpLevel = 0;

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
        {

        }
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
            xpLevel++;
        }
        if (eventCount == 4)
        {
            SpawnEnemy(bruiserPrefab);
            spawnTimer = 2.5f;
        }
        if (eventCount == 5)
        {
            enemyArray = new GameObject[3];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = rangedPrefab;
        }
        if (eventCount == 6)
        {
            enemyArray = new GameObject[4];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = rangedPrefab;
            enemyArray[3] = rangedPrefab;
            xpLevel++;
        }
        if (eventCount == 7)
        {

        }
        if (eventCount == 8) 
        {
            SlimeEvent();
            spawnTimer = 2f;
        }
        if (eventCount == 9)
        {
            enemyArray = new GameObject[4];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = rangedPrefab;
            enemyArray[3] = bruiserPrefab;
            xpLevel++;
        }
        if (eventCount == 10)
        {

        }
        if (eventCount == 11)
        {
            enemyArray = new GameObject[4];
            enemyArray[0] = slimePrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = rangedPrefab;
            enemyArray[3] = bruiserPrefab;
        }
        if (eventCount == 12)
        {
            ArcherEvent();
            spawnTimer = 1.5f;
            xpLevel++;
        }
        if (eventCount == 13)
        {
            enemyArray = new GameObject[3];
            enemyArray[0] = slimePrefab;
            enemyArray[1] = rangedPrefab;
            enemyArray[2] = bruiserPrefab;
        }
        if (eventCount == 14)
        {
            
        }
        if (eventCount == 15)
        {
            KnightEvent();
        }
        if (eventCount == 16)
        {
            spawnTimer = 1f;
        }
        if (eventCount == 17)
        {
            enemyArray = new GameObject[4];
            enemyArray[0] = slimePrefab;
            enemyArray[1] = rangedPrefab;
            enemyArray[2] = bruiserPrefab;
            enemyArray[3] = bruiserPrefab;
        }
        if (eventCount == 18)
        {
            spawnTimer = 0.5f;
        }
        if (eventCount == 19)
        {

        }
        if (eventCount == 20)
        {
            SpawnEnemy(bossPrefab);
        }

        void SlimeEvent()
        {
            for (int i = 28; i >= 0; i--)
            {
                SpawnEnemy(slimePrefab);
            }
        }

        void ArcherEvent()
        {
            for (int i = 20; i >= 0; i--)
            {
                SpawnEnemy(rangedPrefab);
            }
        }

        void KnightEvent()
        {
            for (int i = 12; i >= 0; i--)
            {
                SpawnEnemy(bruiserPrefab);
            }
        }
    }
}
