using UnityEngine;

public class EnergyExplosion : BaseSpell
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entities = new Collider2D[20];
    }

    // Update is called once per frame
    void Update()
    {
        castArea();
    }
}
