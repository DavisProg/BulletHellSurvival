using UnityEngine;

public class RandomTarget : ITarget
{
    LayerMask enemyLayer;
    Collider2D[] arr = new Collider2D[20];

    public RandomTarget(LayerMask enemyLayer)
    {
        this.enemyLayer = enemyLayer;
    }

    public bool GetTarget(Transform caster, float range, out Vector2 position)
    {
        int entitiesCount = Physics2D.OverlapCircleNonAlloc(caster.position, range, arr, enemyLayer);
        if (entitiesCount == 0)
        {
            position = default;
            return false;
        }
        position = arr[Random.Range(0, entitiesCount)].transform.position;
        return true;
    }
}
