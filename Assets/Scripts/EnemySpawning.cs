using System.Collections;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;
    public float radius;
    public float interval;
    public bool canSpawn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        while (canSpawn)
        {
            Vector2 randomPoint = Random.insideUnitCircle * radius;
            Vector3 spawnPoint = transform.position + (Vector3)randomPoint;
            if (isPointVisible(spawnPoint))
            {
                continue;
            }
            Instantiate(enemy, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(interval); 
        }
        
    }
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
