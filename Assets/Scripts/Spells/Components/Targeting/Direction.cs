using System;
using UnityEngine;

public class Direction : ITarget
{
    private float angle;

    public void setAngle(float angle)
    {
        this.angle = angle;
    }
    public bool GetTarget(Transform caster, float range, out Vector2[] position)
    {
        Vector2 pos = new Vector2();
        pos.x = caster.position.x + (range * (float) Math.Sin(angle));
        pos.y = caster.position.y + (range * (float) Math.Cos(angle));


        position = new Vector2[]{pos};
        return true;

    }
}
