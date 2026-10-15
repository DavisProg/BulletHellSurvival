using UnityEngine;
public class MagicMissile : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;
    [SerializeField] int missileAmount;
    [SerializeField] float projectileLifeTime;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer, missileAmount);

        defineCast();
        defineStartingValues();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
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
    }
    public override void firstPathEnable()
    {
        damage = 3;
        defineCast();
    }
    public override void secondPathEnable()
    {
        missileAmount = 3;
        targeting = new NearestTarget(enemyLayer, missileAmount);
        defineCast();
    }
}
