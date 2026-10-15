using UnityEngine;

public class Snowball : BaseSpell
{
   [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;
    [SerializeField] float slowPercentage;
    [SerializeField] float slowDuration;
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
            pierce,
            size,
            projectileLifeTime,
            new IEffect[]
            {
                new DamageEffect(damage, this),
                new SlowEffect(slowPercentage, slowDuration)
            }
        );
    }
    public override void firstPathEnable()
    {
        amount = 5;
        defineCast();
    }
    public override void secondPathEnable()
    {
        slowPercentage = 50;
        defineCast();
    }
}
