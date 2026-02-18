using UnityEngine;

public class DamageEffect : IEffect
{
    float damage;

    public DamageEffect(float damage)
    {
        this.damage = damage;
    }

    public void Apply(GameObject target)
    {
        if (target.TryGetComponent(out Enemy enemy))
        {
            enemy.takeDamage(damage);
        }
    }
}