using System.Collections;
using UnityEngine;

public class DamageOverTimeEffect : IEffect
{
    float damage;
    float damagePerInterval;
    float intervalCooldown;

    public DamageOverTimeEffect(float damage, float damagePerInterval, float intervalCooldown)
    {
        this.damage = damage;
        this.damagePerInterval = damagePerInterval;
        this.intervalCooldown = intervalCooldown;
    }
    IEnumerator damageOverTime(GameObject target)
    {
        float damageLeft = damage;
        Enemy enemy = target.GetComponent<Enemy>();
        while(damageLeft > 0)
        {
            enemy.takeDamage(damagePerInterval);
            damageLeft = damageLeft - damagePerInterval;
            Debug.Log("Damage left to make: " + damageLeft);
            Debug.Log("Enemy health: " + enemy.health);
            yield return new WaitForSeconds(intervalCooldown);
        }
    }
    public void Apply(GameObject target)
    {
        target.GetComponent<Enemy>().StartCoroutine(damageOverTime(target));
    }
}
