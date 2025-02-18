using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AutoReturnToMainMenu(20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AutoReturnToMainMenu(float delay)
    {
        StartCoroutine(ReturnAfterDelay(delay));
    }

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
