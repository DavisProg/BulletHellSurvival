using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Wave
{
    public int waveNumber;
    public List<WaveEnemyData> enemyData;
    public int enemiesSpawnedPerSpawn;
    public float spawnInterval;

    public Wave Clone()
    {
        return new Wave{
            waveNumber = waveNumber,
            enemyData = enemyData
                .Select(enemy => new WaveEnemyData
                {
                    totalEnemies = enemy.totalEnemies,
                    enemiesSpawned = enemy.enemiesSpawned,
                    totalHealth = enemy.totalHealth
                })
            .ToList(),
            enemiesSpawnedPerSpawn = enemiesSpawnedPerSpawn,
            spawnInterval = spawnInterval
        };
    }
}
