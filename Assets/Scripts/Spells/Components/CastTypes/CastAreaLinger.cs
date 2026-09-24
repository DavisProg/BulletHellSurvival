using UnityEngine;

public class CastAreaLinger : ICast
{
    GameObject prefab;
    float size;
    IEffect[] effects;
    float duration;
    float interval;

    public CastAreaLinger(GameObject prefab, float size, float duration, float interval, params IEffect[] effects)
    {
        this.prefab = prefab;
        this.size = size;
        this.duration = duration;
        this.effects = effects;
        this.interval = interval;
    }
    public void Cast(Transform caster, Vector2 target)
    {
        GameObject area  = Object.Instantiate(prefab, target, Quaternion.identity);
        AreaLinger linger = area.GetComponent<AreaLinger>();
        linger.Init(size, effects, duration, interval);
    }   
}