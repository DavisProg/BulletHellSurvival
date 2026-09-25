using System.Collections;
using UnityEngine;

public class DamageOverTimeEffect : IEffect
{
    float damage;
    float damagePerInterval;
    float intervalCooldown;
    BaseSpell source;

    public DamageOverTimeEffect(float damage, float damagePerInterval, float intervalCooldown, BaseSpell source)
    {
        this.damage = damage;
        this.damagePerInterval = damagePerInterval;
        this.intervalCooldown = intervalCooldown;
        this.source = source;
    }
    IEnumerator damageOverTime(GameObject target, BaseSpell source)
    {
        float damageLeft = damage;
        Enemy enemy = target.GetComponent<Enemy>();
        while(damageLeft > 0)
        {
            enemy.takeDamage(damagePerInterval, source);
            damageLeft = damageLeft - damagePerInterval;
            yield return new WaitForSeconds(intervalCooldown);
        }
    }
    public void Apply(GameObject target)
    {
        target.GetComponent<Enemy>().StartCoroutine(damageOverTime(target, source));
    }
}
