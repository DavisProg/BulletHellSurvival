using System;
using UnityEngine;

public class EnemyDeathController : MonoBehaviour
{
    public event Action<BaseSpell, Vector3> enemyDeath;
    public static EnemyDeathController Instance;
    [SerializeField] GameObject Player;
    private EnemySpawning enemySpawning;
    private void Awake()
    {
        Instance = this;
        enemySpawning = Player.GetComponent<EnemySpawning>();
    }

    public void RegisterEnemyDeathEvent(Enemy enemy)
    {
        enemy.onDeath += HandleEnemyDeath;
    }
    public void HandleEnemyDeath(Enemy enemy, BaseSpell source)
    {
        Debug.Log("Recieved death from " + source);
        enemyDeath?.Invoke(source, enemy.transform.position);
        enemySpawning.enemyList.Remove(enemy.gameObject);
        enemy.onDeath -= HandleEnemyDeath;
    }
}
