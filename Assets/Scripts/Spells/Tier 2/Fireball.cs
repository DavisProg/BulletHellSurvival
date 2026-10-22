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
    [SerializeField] int amount;
    [SerializeField] float projectileLifeTime;

    void Awake()
    {
        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void defineCast()
    {
        targeting = new NearestTarget(enemyLayer, amount);
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            1,
            projectileSize,
            projectileLifeTime,
            new IEffect[]
            {
                new RepeatEffect(new CastArea(explosionPrefab, explosionSize, new DamageEffect(damage, this)), 0.1f, new PlayerCenter())
            }
        );
    }
    public override void firstPathEnable()
    {
        damage = 30;
        defineCast();
    }
    public override void secondPathEnable()
    {
        cooldown = 7;
        defineCast();
    }
}
