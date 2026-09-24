using UnityEngine;

public class TurretMissile : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    public float damage;
    public float newCooldown;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;
    [SerializeField] int missileAmount;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer, missileAmount);
    }
    public void defineCast()
    {
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            new IEffect[]
            {
                new DamageEffect(damage, this)
            }
        );
        setTrigger(new onLoop(this, newCooldown));
    }
}
