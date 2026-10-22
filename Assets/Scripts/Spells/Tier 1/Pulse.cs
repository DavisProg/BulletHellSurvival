using UnityEngine;

public class Pulse : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float size;
    [SerializeField] float strength;
    [SerializeField] float delay;

    void Awake()
    {
        defineCast();
        setTrigger(new onEnterRange(this, cooldown, gameObject.transform, range, enemyLayer));
    }
    public override void defineCast()
    {
        targeting = new PlayerCenter();
        casting =  new CastArea(
            explosionPrefab,
            size,
            new KnockbackEffect(strength, delay, gameObject.transform)
        ).SetAnimated(false);
    }
    public override void firstPathEnable()
    {
        strength = 50;
        defineCast();
    }
    public override void secondPathEnable()
    {
        range = 5;
        defineCast();
    }
}

