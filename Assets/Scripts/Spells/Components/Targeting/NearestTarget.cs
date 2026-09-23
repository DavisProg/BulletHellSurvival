using System.Collections.Generic;
using UnityEngine;

public class NearestTarget : ITarget
{
    LayerMask enemyLayer;
    Collider2D[] arr = new Collider2D[20];
    int targetCount;

    public NearestTarget(LayerMask enemyLayer, int targetCount)
    {
        this.enemyLayer = enemyLayer;
        this.targetCount = targetCount;
    }

    public bool GetTarget(Transform caster, float range, out Vector2[] position)
    {
        int entitiesCount = Physics2D.OverlapCircleNonAlloc(caster.position, range, arr, enemyLayer);
        List<Collider2D> nearestObjects = new List<Collider2D>();
        List<Vector2> objectPositions = new List<Vector2>();
        Collider2D obj = null;
        for(int i = 0; i < targetCount; i++)
        {
            float nearestDistance = Mathf.Infinity;

            for (int j = 0; j < entitiesCount; j++){
                float distance = Vector2.Distance(caster.position, arr[j].transform.position);
                if (nearestObjects.Contains(arr[j]))
                    {
                        continue;
                    }
                if (distance < nearestDistance && distance > 1){
                    nearestDistance = distance;
                    obj = arr[j];
                }
            }
            if(obj == null)
            {
                break;
            }
            nearestObjects.Add(obj);
        }
        if (nearestObjects.Count == 0){
            position = default;
            return false;
        }
        foreach(Collider2D col in nearestObjects)
        {
            objectPositions.Add(col.transform.position);
        }

        position = objectPositions.ToArray();
        if (position != null)
        {
            return true;
        }
        return false;
    }
}
