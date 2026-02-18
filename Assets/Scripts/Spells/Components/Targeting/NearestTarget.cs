using UnityEngine;

public class NearestTarget : ITargetable
{
    LayerMask enemyLayer;
    Collider2D[] arr = new Collider2D[20];

    public NearestTarget(LayerMask enemyLayer)
    {
        this.enemyLayer = enemyLayer;
    }

    public bool GetTarget(Transform caster, float range, out Vector2 position)
    {
        float nearestDistance = Mathf.Infinity;
        Collider2D nearestObject = null;

        int entitiesCount = Physics2D.OverlapCircleNonAlloc(caster.position, range, arr, enemyLayer);

        for (int i = 0; i < entitiesCount; i++){
            float distance = Vector2.Distance(caster.position, arr[i].transform.position);
            if (distance < nearestDistance){
                nearestDistance = distance;
                nearestObject = arr[i];
            }
        }
        if (nearestObject == null){
            position = default;
            return false;
        }
        position = nearestObject.transform.position;
        return true;
    }
}
