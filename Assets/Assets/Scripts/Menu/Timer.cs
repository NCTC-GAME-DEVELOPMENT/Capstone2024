using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject zombiePrefab;
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
        if (eventCount == 1) //19 Minutes
        {

        }
        if (eventCount == 2) //18 Minutes
        {
            enemyArray = new GameObject[3];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = crawlerPrefab;
            enemyArray[2] = slimePrefab;
        }
        if (eventCount == 3) //17 Minutes
        {
            enemyArray = new GameObject[2];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            xpLevel++;
        }
        if (eventCount == 4) //16 Minutes
        {
            SpawnEnemy(bruiserPrefab);
            spawnTimer = 2.5f;
        }
        if (eventCount == 5) //15 Minutes
        {
            enemyArray = new GameObject[3];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = zombiePrefab;
        }
        if (eventCount == 6) //14 Minutes
        {
            xpLevel++;
        }
        if (eventCount == 7) //13 Minutes
        {
            enemyArray = new GameObject[4];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = zombiePrefab;
            enemyArray[3] = rangedPrefab;
        }
        if (eventCount == 8) //12 Minutes
        {
            SlimeEvent();
            Invoke("SlimeEvent", 15);
            CancelInvoke(nameof(SpawnRandomEnemy));
            spawnTimer = 2f;
        }
        if (eventCount == 9) //11 Minutes
        {
            InvokeRepeating(nameof(SpawnRandomEnemy), 0f, spawnTimer);
            enemyArray = new GameObject[5];
            enemyArray[0] = crawlerPrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = zombiePrefab;
            enemyArray[3] = rangedPrefab;
            enemyArray[4] = bruiserPrefab;
            xpLevel++;
        }
        if (eventCount == 10) //10 Mintues
        {

        }
        if (eventCount == 11) //9 Minutes
        {
            enemyArray = new GameObject[5];
            enemyArray[0] = slimePrefab;
            enemyArray[1] = slimePrefab;
            enemyArray[2] = zombiePrefab;
            enemyArray[3] = rangedPrefab;
            enemyArray[4] = bruiserPrefab;
        }
        if (eventCount == 12) //8 Minutes
        {
            ZombieEvent();
            CancelInvoke(nameof(SpawnRandomEnemy));
            spawnTimer = 1.5f;
            xpLevel++;
        }
        if (eventCount == 13) //7 Mintues
        {
            InvokeRepeating(nameof(SpawnRandomEnemy), 0f, spawnTimer);
            enemyArray = new GameObject[4];
            enemyArray[0] = slimePrefab;
            enemyArray[1] = zombiePrefab;
            enemyArray[2] = rangedPrefab;
            enemyArray[3] = bruiserPrefab;
        }
        if (eventCount == 14) //6 Minutes
        {
            
        }
        if (eventCount == 15) //5 Minutes
        {
            KnightEvent();
            CancelInvoke(nameof(SpawnRandomEnemy));
        }
        if (eventCount == 16) //4 Minutes
        {
            InvokeRepeating(nameof(SpawnRandomEnemy), 0f, spawnTimer);
            spawnTimer = 1f;
        }
        if (eventCount == 17) //3 Minutes
        {
            enemyArray = new GameObject[5];
            enemyArray[0] = slimePrefab;
            enemyArray[1] = zombiePrefab;
            enemyArray[2] = rangedPrefab;
            enemyArray[3] = bruiserPrefab;
            enemyArray[4] = bruiserPrefab;
        }
        if (eventCount == 18) //2 Minutes
        {
            spawnTimer = 0.5f;
        }
        if (eventCount == 19) //1 Minute
        {

        }
        if (eventCount == 20) //Boss Time
        {
            SpawnEnemy(bossPrefab);
        }

        void SlimeEvent()
        {
            for (int i = 40; i >= 0; i--)
            {
                SpawnEnemy(slimePrefab);
            }
        }

        void ZombieEvent()
        {
            for (int i = 40; i >= 0; i--)
            {
                SpawnEnemy(zombiePrefab);
            }
        }

        void KnightEvent()
        {
            for (int i = 25; i >= 0; i--)
            {
                SpawnEnemy(bruiserPrefab);
            }
        }
    }
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
