using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] Wave[] waveList;
    public List<GameObject> enemyList = new List<GameObject>();
    private int currentWave = 0;
    private int maxEnemyCount = 300;
    public GameObject enemy;
    public float radius;
    public bool canSpawn = true;

    bool isPointVisible(Vector3 point)
    {
        Camera cam = Camera.main;
        Vector3 viewportPos = cam.WorldToViewportPoint(point);

        return viewportPos.z > 0 &&
            viewportPos.x > 0 && viewportPos.x < 1 &&
            viewportPos.y > 0 && viewportPos.y < 1;
    }
    IEnumerator SpawnEnemy()
    {
        Wave wave = waveList[currentWave - 1];
        int spawnedEnemyAmount = 0;
        while (canSpawn)
        {
            for(int i = 0; i < wave.enemiesSpawnedPerSpawn; i++)
            {
                if(enemyList.Count + wave.enemiesSpawnedPerSpawn < maxEnemyCount)
                {
                    Vector2 randomPoint = Random.insideUnitCircle * radius;
                    Vector3 spawnPoint = transform.position + (Vector3)randomPoint;
                    if (isPointVisible(spawnPoint))
                    {
                        i--;
                        continue;
                    }
                    GameObject spawnedEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
                    enemyList.Add(spawnedEnemy);
                    spawnedEnemyAmount++;
                    if (spawnedEnemyAmount >= wave.totalEnemies)
                    {
                        break;
                    }
                }
                else
                {
                    i--;
                    continue;
                }
            }
            yield return new WaitForSeconds(wave.spawnInterval); 
            if(spawnedEnemyAmount >= wave.totalEnemies )
            {
                break;
            }
            
        }
        startNewWave();
    }
    void startNewWave()
    {
        if(currentWave + 1 <= waveList.Count() && canSpawn)
        {
            currentWave++;
            Debug.Log("Starting Wave " + waveList[currentWave - 1].waveNumber);
            StartCoroutine(SpawnEnemy());
        }
    }
    void Start()
    {
        startNewWave();
    }
    private void OnDrawGizmosSelected()
{
    Gizmos.DrawWireSphere(transform.position, radius);
}
}
