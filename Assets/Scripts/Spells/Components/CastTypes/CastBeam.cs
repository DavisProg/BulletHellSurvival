using UnityEngine;
using UnityEngine.PlayerLoop;

public class CastBeam : ICast
{
    private GameObject prefab;
    private float width;
    private float height;
    private float duration;
    private IEffect[] effects;

    public CastBeam(GameObject prefab, float width, float height, float duration, IEffect[] effects)
    {
        this.prefab = prefab;
        this.width = width;
        this.height = height;
        this.duration = duration;
        this.effects = effects;
    }
    public void Cast(Transform caster, Vector2 target)
    {
        GameObject beam  = Object.Instantiate(prefab, caster.position, Quaternion.identity);
        beam.GetComponent<Beam>().Init(target, caster, height, width, duration, effects);
    }
}
