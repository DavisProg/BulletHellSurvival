using UnityEngine;

public class MagicOrb : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;

    void Awake()
    {
        targeting = new RandomPoint(enemyLayer);

        casting = new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            "Enemy",
            new DamageEffect(damage)
        );

        StartCoroutine(tryCast());
    }
}

