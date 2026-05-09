using UnityEditor;
using UnityEngine;
using System.Collections;

public class RepeatEffect : IEffect
{
    ICastable spell;
    ITargetable targeting;
    float range;
    public RepeatEffect(ICastable spell,float range, ITargetable targeting)
    {
        this.spell = spell;
        this.range = range;
        this.targeting = targeting;
    }

    public void Apply(GameObject target)
    {
        if (targeting.GetTarget(target.transform, range, out Vector2 nextTarget)){
            spell.Cast(target.transform, nextTarget);
        }
    }
}
