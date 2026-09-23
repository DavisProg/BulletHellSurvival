using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public int waveNumber;
    public List<WaveEnemyData> enemyData;
    public int enemiesSpawnedPerSpawn;
    public float spawnInterval;
}
