using UnityEngine;

public class KnockbackEffect : IEffect
{
    float strength;
    float delay;
    Transform caster;

    public KnockbackEffect(float strength, float delay, Transform caster)
    {
        this.strength = strength;
        this.delay = delay;
        this.caster = caster;
    }
    public void Apply(GameObject target)
    {
        if (target.TryGetComponent(out Enemy enemy))
        {
            Vector2 direction = target.transform.position - caster.transform.position;
            enemy.takeKnockback(strength, delay, direction);
        }
        }
}
