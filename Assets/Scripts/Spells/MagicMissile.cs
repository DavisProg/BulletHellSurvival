using UnityEngine;
using UnityEngine.UIElements;

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
            new IEffect[]
            {
                new DamageEffect(damage),
                new RepeatEffect(new CastProjectile(projectilePrefab, speed, pierce, size, new DamageEffect(10)), range, new NearestTarget(enemyLayer)) 
            }
        );

        StartCoroutine(tryCast());
    }
}
