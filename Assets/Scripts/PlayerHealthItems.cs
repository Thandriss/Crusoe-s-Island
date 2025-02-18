using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthItems : MonoBehaviour
{
    public int collectedItems;
    [SerializeField]
    private TextMeshProUGUI promptText;
    public PlayerHealth playerHealth;
    public AudioClip healMusic;
    public AudioSource music;
    void Start()
    {
        collectedItems = 0;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            useItem();
        }
        UpdateUI();
    }


    public void TakeItem(Bush bush)
    {
        if (bush.TryTakeBerry())
        {
            collectedItems++;
        }

    }

    public void useItem()
    {
        if (collectedItems != 0) {
            collectedItems--;
            playerHealth.RestoreHealth(20);
            music.clip = healMusic;
            if (music != null && !music.isPlaying)
            {
                music.Play();
            }
        }
    }

    private void UpdateUI()
    {
        if (promptText != null)
        {
            promptText.text = $"Berries: {collectedItems}, Press E to eat";
        }
    }
}
