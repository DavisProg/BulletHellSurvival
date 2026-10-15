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
    List<IEffect> effectList = new List<IEffect>();

    void Awake()
    {
        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void defineCast()
    {
        targeting = new NearestTarget(enemyLayer, 1);
        float currentAngle = 0;
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
    public override void firstPathEnable()
    {
        effectList.Clear();
        splitAmount = 8;
        cooldown = 4;
        defineCast();
    }
    public override void secondPathEnable()
    {
        effectList.Clear();
        float currentAngle = 0;
        effectList.Add(new RepeatEffect(new CastArea(explosionPrefab, explosionSize, duration, new DamageEffect(damage, this)), 0.1f, new PlayerCenter()));
        for(int i = 0; i < splitAmount; i++)
            {
                effectList.Add(
                new RepeatEffect(new CastProjectile(
                    projectilePrefab, speed, 1, projectileSize, projectileLifeTime, new RepeatEffect(
                        new CastArea(explosionPrefab, explosionSize, duration, new DamageEffect(damage, this), new DamageOverTimeEffect(30, 5, 1, this)), range, new PlayerCenter()
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
