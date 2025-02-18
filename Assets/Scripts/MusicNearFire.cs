using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNearFire : MonoBehaviour
{
    public AudioSource music; 
    public string playerTag = "Player"; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (music != null && !music.isPlaying)
            {
                music.Play(); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (music != null && music.isPlaying)
            {
                music.Stop(); 
            }
        }
    }
}
