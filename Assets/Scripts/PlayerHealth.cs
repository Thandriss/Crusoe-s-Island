using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    private float cHealth;
    private float Timer;
    public float cHealthMax = 150f;
    public float speed = 2f;
    public Image frontHealth;
    public Image backHealth;
    [Header("Damege")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float dur;
    public AudioClip deathMusic;
    public AudioSource audio;

    void Start()
    {
        cHealth = cHealthMax;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    void Update()
    {
        cHealth = Mathf.Clamp(cHealth,0, cHealthMax);
        UpdateHealthUI();
        if (cHealth <= 0)
        {
            if (!audio.isPlaying)
            {
                audio.clip = deathMusic;
                audio.Play();
            }
            StartCoroutine(LoadSceneAfterDelay("MainMenu", deathMusic.length));
        }
        if (overlay.color.a > 0) {
            if (cHealth < 30)
                return;
            dur += Time.deltaTime;
            if ( dur > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
        
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealth.fillAmount;
        float fillT = backHealth.fillAmount;
        float hFr = cHealth / cHealthMax;
        if (fillT > hFr) { 
            frontHealth.fillAmount = hFr;
            backHealth.color = Color.red;
            Timer += Time.deltaTime;
            float percentComplete = Timer / speed;
            percentComplete = percentComplete * percentComplete;
            backHealth.fillAmount = Mathf.Lerp(fillT, hFr, percentComplete);
        }
        if(fillF < hFr)
        { 
            if (cHealth <= cHealthMax) {
                backHealth.color = Color.green;
                backHealth.fillAmount = hFr;
                Timer += Time.deltaTime;
                float percentComplete = Timer / speed;
                percentComplete = percentComplete * percentComplete;
                frontHealth.fillAmount = Mathf.Lerp(-fillF, backHealth.fillAmount, percentComplete);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        cHealth -= damage;
        Timer = 0f;
        dur = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(float health) { 
        cHealth += health;
        cHealth = Mathf.Min(cHealth, cHealthMax);
        Timer = 0f;
    }
}
