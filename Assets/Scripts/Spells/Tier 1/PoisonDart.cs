using UnityEngine;

public class PoisonDart : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;
    [SerializeField] float damagePerInterval;
    [SerializeField] float damageDuration;
    [SerializeField] int amount;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer, amount);

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
        casting = new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            new DamageOverTimeEffect(damage, damagePerInterval, damageDuration, this)
        );
    }
}
