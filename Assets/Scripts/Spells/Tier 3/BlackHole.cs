using UnityEngine;

public class BlackHole : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] float pullStrength;
    [SerializeField] float pullInterval;
    [SerializeField] float interval;
    [SerializeField] float duration;

    void Awake()
    {
        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void defineCast()
    {
        targeting = new NearestTarget(enemyLayer, 1);
        casting = new CastAreaLinger(
            explosionPrefab,
            size,
            duration,
            interval,
            new IEffect[]{
            new DamageEffect(damage, this),
            new PullEffect(pullStrength, duration, pullInterval)
            }
        ).SetMovement(speed);
    }
    public override void firstPathEnable()
    {
        speed = 4;
        damage = 60;
        defineCast();
    }
    public override void secondPathEnable()
    {
        cooldown = 10;
    }
}
