using UnityEngine;

public class SummonTurret : BaseSpell
{
    [SerializeField] GameObject turretPrefab;
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
        casting = new CastTurret(
            turretPrefab,
            duration,
            damage,
            interval,
            size
        );
    }
}
