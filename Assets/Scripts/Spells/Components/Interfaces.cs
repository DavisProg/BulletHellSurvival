using System;
using UnityEngine;
public interface ITarget
{
    bool GetTarget(Transform caster, float range, out Vector2[] position);
} 
public interface ICast
{
    void Cast(Transform caster, Vector2 target);
}
public interface IEffect
{
    void Apply(GameObject target, GameObject origin = default);
}
public interface ITrigger
{
    event Action Triggered;
    float cooldown { get; set; }
    float elapsedTime { get; set; }

    void Enable();
    void Disable();
}