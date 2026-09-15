using UnityEditor;
using UnityEngine;

public class MagicOrb : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer);
        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void firstPathEnable()
    {
        pierce += 3;
        defineCast();
    }
    public override void secondPathEnable()
    {
        speed += 3;
        defineCast();
    }
    protected void defineCast()
    {
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            new DamageEffect(damage, this)
        );
    }
}
