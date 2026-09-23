using UnityEngine;
using System;
using System.Collections;

public class onEnterRange : ITrigger
{
    public event Action Triggered;
    private MonoBehaviour owner;
    private float cooldown;
    private bool canCast = true;
    Transform caster;
    private Coroutine routine;
    float range;
    LayerMask enemyLayer;
    Collider2D[] arr = new Collider2D[50];
    


    public onEnterRange(MonoBehaviour owner, float cooldown, Transform caster, float range, LayerMask enemyLayer)
    {
        this.owner = owner;
        this.cooldown = cooldown;
        this.caster = caster;
        this.range = range;
        this.enemyLayer = enemyLayer;
    }

    public void Enable()
    {
        if (routine == null)
        {
            routine = owner.StartCoroutine(Tick());
        }
    }
    public void Disable()
    {
        if (routine != null)
        {
            owner.StopCoroutine(routine);
            routine = null;
        }
    }
    private IEnumerator Tick()
    {
        while (true)
        {
            int entitiesCount = Physics2D.OverlapCircleNonAlloc(caster.position, range, arr, enemyLayer);
            for(int i = 0; i < entitiesCount; i++)
            {
                if (!arr[i].isTrigger)
                {
                    Debug.Log($"onEnterRange detected {arr[0].gameObject.name}");
                    Triggered?.Invoke();
                    yield return new WaitForSeconds(cooldown);
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
