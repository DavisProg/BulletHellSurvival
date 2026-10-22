using UnityEngine;

public class PlayerDamageKnockback : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float size;
    [SerializeField] float strength;
    [SerializeField] float delay;
    void Awake()
    {
        defineCast();
        setTrigger(new onPlayerDamage(gameObject.GetComponent<Player>()));
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
}
