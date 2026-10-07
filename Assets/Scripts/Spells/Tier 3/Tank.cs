using UnityEngine;

public class Tank : BaseSpell
{
    [SerializeField] GameObject turretPrefab;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float interval;
    [SerializeField] float duration;

    void Awake()
    {
        targeting = new RandomPoint();

        defineCast();
        setTrigger(new onLoop(this, cooldown));
    }
    public override void tryCast()
    {
        if (level > 0)
        {
            while (true)
            {
                if(targeting.GetTarget(transform, range, out Vector2[] target)){
                    bool completedSuccesfully = true;
                    foreach(Vector2 targetPos in target)
                    {
                        if (Vector2.Distance(targetPos, gameObject.transform.position) <= 5)
                        {
                            completedSuccesfully = false;
                            break;
                        }
                        Debug.Log("Distance: " + Vector2.Distance(targetPos, gameObject.transform.position));
                        casting.Cast(transform, targetPos);
                    }
                    if (completedSuccesfully)
                    {
                        break;
                    }
                }
            }
        }
    }
    protected void defineCast()
    {
        casting = new CastTurret(
            turretPrefab,
            duration,
            damage,
            interval,
            size
        );
    }
}
