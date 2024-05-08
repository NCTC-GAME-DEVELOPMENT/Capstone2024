using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClip[] playerDamage;
    [SerializeField] private AudioClip playerDeath;
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioSource enemySource;
    private bool isPlayingSoundPlayer;
    private bool isPlayingSoundEnemy;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isPlayingSoundPlayer = false;
        isPlayingSoundEnemy = false;
        isDead = false;
    }

    public void PlayerDamageSound()
    {
        if (isPlayingSoundPlayer || isDead)
        {
            return;
        }
        else
        {
            isPlayingSoundPlayer = true;
            int random = Random.Range(1, 4);
            playerSource.PlayOneShot(playerDamage[random - 1]);
            Invoke("ResetPlayerSound", playerDamage[random - 1].length);
        }
    }
    void ResetPlayerSound()
    {
        isPlayingSoundPlayer = false;
    }
    public void EnemyDamageSound(AudioClip enemyAudio)
    {
        if (isPlayingSoundEnemy)
        {
            return;
        }
        else
        {
            isPlayingSoundEnemy = true;
            enemySource.PlayOneShot(enemyAudio);
            Invoke("ResetEnemySound", enemyAudio.length);
        }
    } 
    void ResetEnemySound()
    {
        isPlayingSoundEnemy = false;
    }

    public void PlayerDeathSound()
    {
        if (!isDead)
        {
            isDead = true;
            playerSource.PlayOneShot(playerDeath);
        }
    }
}
