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
        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void defineCast()
    {
        targeting = new RandomPoint();
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
    public override void firstPathEnable()
    {
        damage = 15;
        defineCast();
    }
    public override void secondPathEnable()
    {
        casting = new CastAreaLinger(
            explosionPrefab,
            size,
            duration,
            interval,
            new IEffect[]{
            new DamageEffect(damage, this),
            new SlowEffect(25, duration)
            }
        );
        cooldown = 7;
    }
}
