using UnityEngine;

public class FreezeRay : BaseSpell
{
    [SerializeField] GameObject beamPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] float duration;
    [SerializeField] float slowDuration;
    [SerializeField] int amount;

    void Awake()
    {
        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void defineCast()
    {
        targeting = new NearestTarget(enemyLayer, amount);
        casting =  new CastBeam(
            beamPrefab,
            width,
            height,
            duration,
            new IEffect[]
            {
                new DamageEffect(damage, this),
                new SlowEffect(100, slowDuration)
            }
        );
    }
    public override void firstPathEnable()
    {
        width = 10;
        defineCast();
    }
    public override void secondPathEnable()
    {
        slowDuration = 10;
        defineCast();
    }
}
