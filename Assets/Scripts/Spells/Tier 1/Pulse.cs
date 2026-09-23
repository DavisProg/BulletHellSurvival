using UnityEngine;

public class Pulse : BaseSpell
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
        setTrigger(new onEnterRange(this, cooldown, gameObject.transform, range, enemyLayer));
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}

