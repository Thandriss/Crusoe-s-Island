using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private AudioSource audioSource; 

    public AudioClip attackMusic;   

    public AudioClip normalMusic;   

    private HashSet<Enemy> attackingEnemies = new HashSet<Enemy>(); 

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        PlayNormalMusic();
    }

    public void NotifyAttackStarted(Enemy enemy)
    {
        attackingEnemies.Add(enemy);
        PlayAttackMusic();
    }

    public void NotifyAttackStopped(Enemy enemy)
    {
        attackingEnemies.Remove(enemy);

        if (attackingEnemies.Count == 0)
        {
            PlayNormalMusic();
        }
    }

    private void PlayAttackMusic()
    {
        if (audioSource.clip != attackMusic)
        {
            audioSource.clip = attackMusic;
            audioSource.Play();
        }
    }

    public void PlayNormalMusic()
    {
        if (audioSource.clip != normalMusic)
        {
            audioSource.clip = normalMusic;
            audioSource.Play();
        }
    }
}

