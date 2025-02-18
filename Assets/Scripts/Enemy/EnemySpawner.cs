using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public int enemyNumber;
    public GameObject enemy;

    public int maxEnemies = 5;
    public float spawnDelay = 1f;
    private LightManager lightManager;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private MusicManager musicManager;
    void Start()
    {
        lightManager = FindObjectOfType<LightManager>();
        if (lightManager == null)
        {
            Debug.LogError("LightManager not found in the scene.");
            return;
        }
        musicManager = FindObjectOfType<MusicManager>();


    }

    void Update()
    {
        if (IsNightTime()) { 
            StartCoroutine(SpawnEnemies());
        }
        
        if (!IsNightTime() && spawnedEnemies.Count > 0)
        {
            DespawnAllEnemies();
            musicManager?.PlayNormalMusic();
        }
    }
    private IEnumerator SpawnEnemies()
    {
        Debug.Log(IsNightTime());
        if(IsNightTime())
        {
            while (enemyNumber < maxEnemies)
            {
                Vector3 pos = transform.position;
                Debug.Log(pos.normalized);
                xPos = (int)(pos.x + Random.Range(-16, 13));
                yPos = (int)(pos.y + Random.Range(-20, 1));
                GameObject newEnemy = Instantiate(enemy, pos, Quaternion.identity);
                spawnedEnemies.Add(newEnemy);
                yield return new WaitForSeconds(1);
                enemyNumber++;
            }
        }
        yield return new WaitForSeconds(spawnDelay);
    }

    private bool IsNightTime()
    {
        return !(lightManager.TimeOfDay <= 1162 && lightManager.TimeOfDay >= 286);
    }

    private void DespawnAllEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();
        enemyNumber = 0; 

    }
}
