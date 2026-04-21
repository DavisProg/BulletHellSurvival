using UnityEngine;

public class EnergyExplosion : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float duration;

    void Awake()
    {
        targeting = new RandomPoint(enemyLayer);

        casting = new CastArea(
            explosionPrefab,
            size,
            duration,
            new IEffect[]{
            new DamageEffect(damage),
            new SlowEffect(100, 1)
            }
        );

        StartCoroutine(tryCast());
    }
}
