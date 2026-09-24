using UnityEngine;

public class CastTurret : ICast
{
    GameObject prefab;
    float duration;
    float damage;
    float cooldown;
    float size;

    public CastTurret(GameObject prefab, float duration, float damage, float cooldown, float size)
    {
        this.prefab = prefab;
        this.duration = duration;
        this.damage = damage;
        this.size = size;
        this.cooldown = cooldown;
    }
    public void Cast(Transform caster, Vector2 target)
    {
        GameObject turret  = Object.Instantiate(prefab, caster.position, Quaternion.identity);
        turret.GetComponent<Turret>().Init(size, duration, damage, cooldown);
    }
}
