using UnityEngine;

public class FreezeRay : BaseSpell
{
    [SerializeField] GameObject beamPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] float duration;
    [SerializeField] float reducePercentage;
    [SerializeField] float slowDuration;
    [SerializeField] int amount;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer, amount);

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
        casting =  new CastBeam(
            beamPrefab,
            width,
            height,
            duration,
            new IEffect[]
            {
                new DamageEffect(damage, this),
                new SlowEffect(reducePercentage, slowDuration)
            }
        );
    }
}
