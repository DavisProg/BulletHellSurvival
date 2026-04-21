using UnityEngine;

public class RandomPoint : ITargetable
{
    LayerMask enemyLayer;
    Collider2D[] arr = new Collider2D[20];

    public RandomPoint(LayerMask enemyLayer)
    {
        this.enemyLayer = enemyLayer;
    }

    public bool GetTarget(Transform caster, float range, out Vector2 position)
    {
        position = (Vector2)caster.position + Random.insideUnitCircle * range;
        return true;
    }
}
