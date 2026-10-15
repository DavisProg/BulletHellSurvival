using UnityEngine;

public class TurretMissile : BaseSpell
{
    public GameObject projectilePrefab;
    public LayerMask enemyLayer;
    public float damage;
    public float newCooldown;
    public float size;
    public float speed;
    public int pierce;
    public int missileAmount;
    public float projectileLifeTime;

    public override void defineCast()
    {
        targeting = new NearestTarget(enemyLayer, missileAmount);
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            projectileLifeTime,
            new IEffect[]
            {
                new DamageEffect(damage, this)
            }
        );
        setTrigger(new onLoop(this, newCooldown));
    }
}
