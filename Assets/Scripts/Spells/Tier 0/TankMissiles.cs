using UnityEngine;

public class TankMissiles : TurretMissile
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionSize;

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
                new RepeatEffect(new CastArea(explosionPrefab, explosionSize, new DamageEffect(damage, this)).SetAnimated(false), 0.1f, new PlayerCenter())
            }
        );
        setTrigger(new onLoop(this, newCooldown));
    }
}
