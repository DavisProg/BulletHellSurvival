using UnityEngine;

public class DamageEffect : IEffect
{
    float damage;
    BaseSpell source;

    public DamageEffect(float damage, BaseSpell source)
    {
        this.damage = damage;
        this.source = source;
    }

    public void Apply(GameObject target)
    {
        if (target.TryGetComponent(out Enemy enemy))
        {
            enemy.takeDamage(damage, source);
        }
    }
}