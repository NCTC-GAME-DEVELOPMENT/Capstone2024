using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioClip playerDamage;
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioSource enemySource;
    private bool isPlayingSoundPlayer;
    private bool isPlayingSoundEnemy;
    // Start is called before the first frame update
    void Start()
    {
        isPlayingSoundPlayer = false;
        isPlayingSoundEnemy = false;
    }

    public void PlayerDamageSound()
    {
        if (isPlayingSoundPlayer)
        {
            return;
        }
        else
        {
            isPlayingSoundPlayer = true;
            playerSource.PlayOneShot(playerDamage);
            Invoke("ResetPlayerSound", playerDamage.length);
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
}
