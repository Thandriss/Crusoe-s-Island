using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public string endSceneName = "EndScene";

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= 1000)
        {
            LoadEndScene();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    private void LoadEndScene()
    {
        SceneManager.LoadScene(endSceneName);
    }

}
