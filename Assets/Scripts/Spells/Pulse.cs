using System.Drawing;
using NUnit.Framework;
using UnityEngine;

public class Pulse : BaseSpell
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float size;
    [SerializeField] float duration;
    void Awake()
    {
        targeting = new RandomPoint(enemyLayer);

        level = 1;

        defineCast();
    }
    protected void defineCast()
    {
        casting =  new CastArea(
            explosionPrefab,
            size,
            duration,
            new KnockbackEffect(4, 0.15f, gameObject.transform)
        );
    }
}
