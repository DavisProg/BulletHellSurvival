using UnityEngine;

public class InflictDisease : BaseSpell
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        entities = new Collider2D[20];
    }

    // Update is called once per frame
    void Update()
    {
        castProjectile();
    }
}
