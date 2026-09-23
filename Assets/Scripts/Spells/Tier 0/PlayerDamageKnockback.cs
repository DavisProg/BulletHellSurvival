using UnityEngine;

public class PlayerDamageKnockback : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float size;
    [SerializeField] float duration;
    [SerializeField] float strength;
    [SerializeField] float delay;
    void Awake()
    {
        targeting = new PlayerCenter();
        defineCast();
        setTrigger(new onPlayerDamage(gameObject.GetComponent<Player>()));
    }
    protected void defineCast()
    {
        casting =  new CastArea(
            explosionPrefab,
            size,
            duration,
            new KnockbackEffect(strength, delay, gameObject.transform)
        );
    }
}
