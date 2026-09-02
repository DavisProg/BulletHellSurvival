using System;
using UnityEngine;

public class EnemyDeathController : MonoBehaviour
{
    public event Action<BaseSpell, Vector3> enemyDeath;
    public static EnemyDeathController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemyDeathEvent(Enemy enemy)
    {
        enemy.onDeath += HandleEnemyDeath;
    }
    public void HandleEnemyDeath(Enemy enemy, BaseSpell source)
    {
        Debug.Log("Recieved death from " + source);
        enemyDeath?.Invoke(source, enemy.transform.position);
        enemy.onDeath -= HandleEnemyDeath;
    }
}
