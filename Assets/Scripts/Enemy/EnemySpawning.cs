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
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();
    List<GameObject> enemyCurrentTypes;
    [SerializeField] float radius;
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
        enemyCurrentTypes = new List<GameObject>(enemyTypes);
        while (enemyCurrentTypes.Count > wave.enemyData.Count)
        {       
            enemyCurrentTypes.RemoveAt(enemyCurrentTypes.Count - 1);
        }
        
        
        while (canSpawn)
        {
            float futureAmount = enemyList.Count + wave.enemiesSpawnedPerSpawn;
            for(int i = 0; i < wave.enemiesSpawnedPerSpawn; i++)
            {
                if(futureAmount <= maxEnemyCount)
                {
                    Vector2 randomPoint = Random.insideUnitCircle * radius;
                    Vector3 spawnPoint = transform.position + (Vector3)randomPoint;
                    int randomIndex = Random.Range(0, wave.enemyData.Count);
                    Debug.Log("Index generated: " + randomIndex);
                    if(wave.enemyData[randomIndex].totalEnemies == 0)
                    {
                        wave.enemyData.RemoveAt(randomIndex);
                        enemyCurrentTypes.RemoveAt(randomIndex);
                        i--;
                        continue;
                    }
                    if (isPointVisible(spawnPoint))
                    {
                        i--;
                        continue;
                    }
                    Debug.Log(
                    "Random Index: " + randomIndex + "\n" +
                    "Current Enemy Types: " );
                    foreach(GameObject enemy in enemyCurrentTypes)
                    {
                        Debug.Log(enemy + "\n");
                    }
                    GameObject spawnedEnemy = Instantiate(enemyCurrentTypes[randomIndex], spawnPoint, Quaternion.identity);
                    spawnedEnemy.GetComponent<Enemy>().health = wave.enemyData[randomIndex].totalHealth;
                    wave.enemyData[randomIndex].enemiesSpawned++;
                    enemyList.Add(spawnedEnemy);
                
                    if (wave.enemyData[randomIndex].enemiesSpawned >= wave.enemyData[randomIndex].totalEnemies)
                    {
                        wave.enemyData.RemoveAt(randomIndex);
                        enemyCurrentTypes.RemoveAt(randomIndex); 
                        if(wave.enemyData.Count == 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    i--;
                    yield return new WaitForSeconds(wave.spawnInterval); 
                    continue;
                }
            }
            yield return new WaitForSeconds(wave.spawnInterval); 
            if(wave.enemyData.Count == 0)
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
