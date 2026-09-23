using UnityEngine;

public class EnergyExplosion : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float duration;
    [SerializeField] BaseSpell parentSpell;

    void Awake()
    {
        targeting = new RandomPoint(enemyLayer);

        defineCast();
        setTrigger(new onEnemyDeath(parentSpell));
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
