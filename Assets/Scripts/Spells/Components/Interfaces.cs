using System;
using UnityEngine;
public interface ITarget
{
    bool GetTarget(Transform caster, float range, out Vector2 position);
}
public interface ICast
{
    void Cast(Transform caster, Vector2 target);
}
public interface IEffect
{
    void Apply(GameObject target);
}
public interface ITrigger
{
    event Action Triggered;
    
    void Enable();
    void Disable();
}

