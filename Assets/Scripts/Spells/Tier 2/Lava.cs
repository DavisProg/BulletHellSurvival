using UnityEngine;

public class Lava : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float interval;
    [SerializeField] float duration;

    void Awake()
    {
        targeting = new RandomPoint();

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
        casting = new CastAreaLinger(
            explosionPrefab,
            size,
            duration,
            interval,
            new IEffect[]{
            new DamageEffect(damage, this)
            }
        );
    }
}
