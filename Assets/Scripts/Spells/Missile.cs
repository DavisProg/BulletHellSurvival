using UnityEngine;

public class Missile : BaseSpell
{
    void Awake()
    {
        entities = new Collider2D[20];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        castProjectile();
    }
}
