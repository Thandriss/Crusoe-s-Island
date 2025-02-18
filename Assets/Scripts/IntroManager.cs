using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;

    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadScene("Game");
    }
}
