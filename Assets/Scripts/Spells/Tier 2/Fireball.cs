using UnityEngine;

public class Fireball : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float projectileSize;
    [SerializeField] float explosionSize;
    [SerializeField] float speed;
    [SerializeField] float duration;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer);

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            1,
            projectileSize,
            new IEffect[]
            {
                new RepeatEffect(new CastArea(explosionPrefab, explosionSize, duration, new DamageEffect(damage, this)), 0.1f, new PlayerCenter())
            }
        );
    }
}
