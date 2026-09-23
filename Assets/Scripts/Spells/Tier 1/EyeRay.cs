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
            }
        );
    }
}
