using UnityEngine;

public class RandomPoint : ITarget
{
    public RandomPoint()
    {
    }
    public bool GetTarget(Transform caster, float range, out Vector2[] position)
    {
        position = new Vector2[]{(Vector2)caster.position + Random.insideUnitCircle * range};
        return true;
    }
}
