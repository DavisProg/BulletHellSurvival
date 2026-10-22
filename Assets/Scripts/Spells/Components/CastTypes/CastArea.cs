using UnityEngine;

public class CastArea : ICast
{
    GameObject prefab;
    float size;
    bool animated = true;
    IEffect[] effects;

    public CastArea(GameObject prefab, float size, params IEffect[] effects)
    {
        this.prefab = prefab;
        this.size = size;
        this.effects = effects;
    }
    public CastArea SetAnimated(bool animated){
        this.animated = animated;
        return this;
    }
    public void Cast(Transform caster, Vector2 target)
    {
        GameObject area  = Object.Instantiate(prefab, target, Quaternion.identity);
        Splash splash = area.GetComponent<Splash>();
        splash.Init(size, effects, animated);
    }   
}
