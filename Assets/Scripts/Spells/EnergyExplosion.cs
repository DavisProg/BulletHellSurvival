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
        targeting = new RandomTarget(enemyLayer);

        casting = new CastArea(
            explosionPrefab,
            size,
            duration,
            new DamageEffect(damage)
        );

        StartCoroutine(tryCast());
    }
}
