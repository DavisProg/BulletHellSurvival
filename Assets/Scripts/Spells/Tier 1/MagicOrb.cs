using UnityEngine;

public class MagicOrb : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;
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
            new DamageEffect(damage, this)
        );
    }
    public override void firstPathEnable()
    {
        damage = 10;
        defineCast();
    }
    public override void secondPathEnable()
    {
        speed = 5;
        defineCast();
    }
}
