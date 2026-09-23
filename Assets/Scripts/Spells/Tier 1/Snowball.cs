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

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer, amount);

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            new IEffect[]
            {
                new DamageEffect(damage, this),
                new SlowEffect(slowPercentage, slowDuration)
            }
        );
    }
}
