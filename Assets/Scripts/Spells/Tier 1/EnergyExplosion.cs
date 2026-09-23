using UnityEngine;

public class EnergyExplosion : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float duration;

    void Awake()
    {
        targeting = new RandomPoint();

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
        casting = new CastArea(
            explosionPrefab,
            size,
            duration,
            new IEffect[]{
            new DamageEffect(damage, this)
            }
        );
    }
}
