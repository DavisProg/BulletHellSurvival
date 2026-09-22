using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int waveNumber;
    public List<WaveEnemyData> enemyData;
    public int enemiesSpawnedPerSpawn;
    public float spawnInterval;
}
