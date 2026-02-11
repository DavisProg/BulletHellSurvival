using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    public string title;
    public string description;
    public bool known;
    public int amount;
    public float damage;
    public float speed;
    public float range;
    public float cooldown;
    public int pierce;
    public bool grouped;
    public bool canShoot = true;
    public Collider2D[] entities;
    public Collider2D nearestObject;
    public float nearestDistance;
    public LayerMask enemyLayer;
    public GameObject projectile;
void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, range);
}
    public IEnumerator spellCooldown(){
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
    /*
    void init(float damage, float speed, float range, float cooldown, GameObject projectile)
    {
        known = true;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.cooldown = cooldown;
        this.projectile = projectile;
    }
    */
    public IEnumerator createProjectile(Vector2 bulletDirection, int projectileAmount)
    {
        while (projectileAmount > 0)
        {
            GameObject proj  = Instantiate(projectile, transform.position, Quaternion.identity);
            Projectile bullet = proj.GetComponent<Projectile>();
            bullet.Init(bulletDirection, damage, speed, pierce);
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
        
    void Start()
    {
    }
}
