using UnityEngine;

public class EyeRay : BaseSpell
{
    [SerializeField] GameObject beamPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] float duration;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer);

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
