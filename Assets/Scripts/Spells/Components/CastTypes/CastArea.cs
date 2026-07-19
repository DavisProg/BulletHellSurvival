using UnityEngine;

public class CastArea : ICast
{
    GameObject prefab;
    float size;
    IEffect[] effects;
    float duration;

    public CastArea(GameObject prefab, float size, float duration, params IEffect[] effects)
    {
        this.prefab = prefab;
        this.size = size;
        this.duration = duration;
        this.effects = effects;
    }
    public void Cast(Transform caster, Vector2 target)
    {
        GameObject area  = Object.Instantiate(prefab, target, Quaternion.identity);
        Splash splash = area.GetComponent<Splash>();
        splash.Init(size, effects, duration);
    }   
}
