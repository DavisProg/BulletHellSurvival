using UnityEngine;

public class EnergyExplosion : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;

    void Awake()
    {
        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void defineCast()
    {
        targeting = new RandomPoint();
        casting = new CastArea(
            explosionPrefab,
            size,
            new IEffect[]{
            new DamageEffect(damage, this)
            }
        );
    }
    public override void firstPathEnable()
    {
        damage = 5;
        defineCast();
    }
    public override void secondPathEnable()
    {
        cooldown = 7;
        defineCast();
    }
}
