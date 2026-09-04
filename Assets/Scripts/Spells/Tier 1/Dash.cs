using UnityEngine;
using System;

public class Dash : BaseSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float damage;
    [SerializeField] float size;
    [SerializeField] float speed;
    [SerializeField] int pierce;

    //Left off at implementing onKeyDown action to be able to test if code is working

    public event Action onKeyDown;

    void Awake()
    {
        targeting = new Direction();

        defineCast();
        setTrigger(new onKeyboardButtonPress(this));
    }
    public override void tryCast()
    {
        if (level > 0)
        {
            if (targeting is Direction direction)
            {
                Vector2 playerInput = gameObject.GetComponent<Player>().input;
                direction.setAngle(Mathf.Atan2(playerInput.x, playerInput.y));
            }
            if(targeting.GetTarget(transform, range, out Vector2 target)){
                    casting.Cast(transform, target);
                    Debug.Log("Dash casted");
            }
        }
    }
    protected void defineCast()
    {
        casting =  new CastProjectile(
            projectilePrefab,
            speed,
            pierce,
            size,
            new IEffect[]
            {
                new DamageEffect(damage, this),
                new SlowEffect(60, 2),
                new KnockbackEffect(3, 0.1f, gameObject.transform)
            }
        );
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            onKeyDown?.Invoke();
            Debug.Log("E pressed");
        }
    }
}
