using UnityEngine;

public class CastProjectile : ICast
{
    GameObject prefab;

    float speed;
    int pierce;
    float size;
    IEffect[] effects;

    public CastProjectile(GameObject prefab, float speed, int pierce, float size, params IEffect[] effects)
    {
        this.prefab = prefab;
        this.speed = speed;
        this.pierce = pierce;
        this.size = size;
        this.effects = effects;
    }
    public void Cast(Transform caster, Vector2 target)
    {
        GameObject proj  = Object.Instantiate(prefab, caster.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Init(target, caster, speed, pierce, size, effects);
    }
}
