using UnityEngine;
public class RepeatEffect : IEffect
{
    ICast spell;
    ITarget targeting;
    float range;
    public RepeatEffect(ICast spell,float range, ITarget targeting)
    {
        this.spell = spell;
        this.range = range;
        this.targeting = targeting;
    }

    public void Apply(GameObject target)
    {
        if (targeting.GetTarget(target.transform, range, out Vector2[] nextTarget)){
            foreach(Vector2 targetPos in nextTarget)
            {
                spell.Cast(target.transform, targetPos);
            } 
        }
    }
}
