using UnityEngine;

public class TankMissiles : TurretMissile
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionSize;
    [SerializeField] float duration;

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
                new RepeatEffect(new CastArea(explosionPrefab, explosionSize, duration, new DamageEffect(damage, this)), 0.1f, new PlayerCenter())
            }
        );
        setTrigger(new onLoop(this, newCooldown));
    }
}
