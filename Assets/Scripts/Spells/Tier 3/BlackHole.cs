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
        targeting = new NearestTarget(enemyLayer, 1);

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
            new DamageEffect(damage, this),
            new PullEffect(pullStrength, duration, pullInterval)
            }
        ).SetMovement(speed);
    }
}
