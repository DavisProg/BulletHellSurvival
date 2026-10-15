using UnityEngine;

public class EyeRay : BaseSpell
{
    [SerializeField] GameObject beamPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] float duration;
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
            }
        );
    }
    public override void firstPathEnable()
    {
        amount = 2;
        defineCast();
    }
    public override void secondPathEnable()
    {
        damage = 5;
        defineCast();
    }
}
