using UnityEngine;

public class MagicMissile : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer);

        casting = new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            new DamageOverTimeEffect(damage, 1, 0.5f)
        );

        StartCoroutine(tryCast());
    }
}
