using System;
using System.Numerics;
using UnityEngine;

public class onEnemyDeath : ITrigger
{
    public event Action Triggered;
    private Enemy enemy;
    BaseSpell targetSpell;
    EnemyDeathController deathManager;

    public onEnemyDeath(BaseSpell targetSpell)
    {
        this.targetSpell = targetSpell;
        deathManager = EnemyDeathController.Instance;

    }
    public void Enable()
    {
        deathManager.enemyDeath += checkSpell;
        Debug.Log("Im looking for " + targetSpell);
    }
    public void Disable()
    {
        deathManager.enemyDeath -= checkSpell;
    }
    private void checkSpell(BaseSpell spell, UnityEngine.Vector3 deathPos)
    {
        Debug.Log("Recieved that enemy died from " + spell + " and Im looking for " + targetSpell);
        if (spell == targetSpell)
        {
            Triggered?.Invoke();
        }
    }


}
