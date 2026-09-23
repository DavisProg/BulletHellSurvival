using UnityEngine;

public class PlayerCenter: ITarget
{
    public bool GetTarget(Transform caster, float range, out Vector2[] position)
    {
        position = new Vector2[]{(Vector2)caster.position};
        return true;
    }
}