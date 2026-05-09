using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public abstract class BaseSpell : MonoBehaviour
{
    /*
    public IEnumerator createProjectile(Vector2 bulletDirection, int projectileAmount)
    {
        while (projectileAmount > 0)
        {
            GameObject proj  = Instantiate(summonObject, transform.position, Quaternion.identity);
            Projectile bullet = proj.GetComponent<Projectile>();
            bullet.Init(bulletDirection, damage, speed, pierce, damageOverTime, damageOverTimeInterval, damagePerInterval);
            projectileAmount--;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void castProjectile()
    {
        if (canShoot){
            nearestDistance = Mathf.Infinity;
            nearestObject = null;

            int entitiesCount = Physics2D.OverlapCircleNonAlloc(transform.position, range, entities, enemyLayer);
            for (int i = 0; i < entitiesCount; i++)
            {
                float distance = Vector2.Distance(transform.position, entities[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObject = entities[i];
                }
            }
            if (nearestObject == null)
            {
                return;
            }

            Vector2 bulletDirection = (nearestObject.transform.position - transform.position).normalized;
            StartCoroutine(createProjectile(bulletDirection, amount));
            canShoot = false;

            StartCoroutine(spellCooldown());
        }
    }
*/
    [SerializeField] protected bool known;
    [SerializeField] protected float range;
    [SerializeField] protected float cooldown;

    protected ITargetable targeting;
    protected ICastable casting;

    bool canCast = true;

    protected IEnumerator tryCast()
    {
        if (known)
        {
            Debug.Log("Hello");
            while (canCast)
            {
                if(targeting.GetTarget(transform, range, out Vector2 target)){
                    casting.Cast(transform, target);
                }
                canCast = false;
                yield return new WaitForSeconds(cooldown);
                canCast = true;
            }
        }
    }
    public void enable()
    {
        known = true;
        StartCoroutine(tryCast());
    }
}