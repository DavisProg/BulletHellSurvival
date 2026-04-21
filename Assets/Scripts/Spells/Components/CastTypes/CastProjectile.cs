using UnityEngine;

public class CastProjectile : ICastable
{
    GameObject prefab;

    float speed;
    int pierce;
    float size;
    IEffect[] effects;
    string enemyTag;

    public CastProjectile(GameObject prefab, float speed, int pierce, float size, string enemyTag, params IEffect[] effects)
    {
        this.prefab = prefab;
        this.speed = speed;
        this.pierce = pierce;
        this.size = size;
        this.enemyTag = enemyTag;
        this.effects = effects;
    }
    public void Cast(Transform caster, Vector2 target)
    {
        GameObject proj  = Object.Instantiate(prefab, caster.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Init(target, caster, speed, pierce, size, effects, enemyTag);
    }
}
