using UnityEngine;

public class Geobrah : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float speed;
    [SerializeField] float size;
    [SerializeField] int pierce;
    [SerializeField] int projectileCount;
    [SerializeField] float damage;
    [SerializeField] float projectileLifeTime;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer, projectileCount);
        defineCast();
        setTrigger(new onEnterRange(this, cooldown, gameObject.transform, range, enemyLayer));
    }
    protected void defineCast()
    {
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            projectileLifeTime,
            new DamageEffect(damage, this)
        );
    }
}
