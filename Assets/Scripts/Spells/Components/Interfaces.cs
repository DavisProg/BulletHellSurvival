using UnityEngine;
public interface ITargetable
{
    bool GetTarget(Transform caster, float range, out Vector2 position);
}
public interface ICastable
{
    void Cast(Transform caster, Vector2 target);
}
public interface IEffect
{
    void Apply(GameObject target);
}

