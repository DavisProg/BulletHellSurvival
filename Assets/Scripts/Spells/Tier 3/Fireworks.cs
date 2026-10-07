using System.Collections.Generic;
using UnityEngine;

public class Fireworks : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float projectileSize;
    [SerializeField] float explosionSize;
    [SerializeField] float speed;
    [SerializeField] float duration;
    [SerializeField] int splitAmount;
    [SerializeField] float deviationAngle;
    [SerializeField] float projectileLifeTime;

    void Awake()
    {
        targeting = new NearestTarget(enemyLayer, 1);

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    protected void defineCast()
    {
        float currentAngle = 0;
        List<IEffect> effectList = new List<IEffect>();
        effectList.Add(new RepeatEffect(new CastArea(explosionPrefab, explosionSize, duration, new DamageEffect(damage, this)), 0.1f, new PlayerCenter()));
        for(int i = 0; i < splitAmount; i++)
            {
                effectList.Add(
                new RepeatEffect(new CastProjectile(
                    projectilePrefab, speed, 1, projectileSize, projectileLifeTime, new RepeatEffect(
                        new CastArea(explosionPrefab, explosionSize, duration, new DamageEffect(damage, this)), range, new PlayerCenter()
                    )), range, new Direction(currentAngle + deviationAngle))
                    );
                currentAngle += deviationAngle;
            }

        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            1,
            projectileSize,
            projectileLifeTime,
            effectList.ToArray()
        );
    }
}
