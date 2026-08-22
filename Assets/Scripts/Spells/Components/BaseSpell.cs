using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField] protected int level = 0;
    [SerializeField] protected float range;
    [SerializeField] protected float cooldown;

    protected ITarget targeting;
    protected ICast casting;
    protected ITrigger trigger;

    public void setTrigger(ITrigger newTrigger)
    {
        if(trigger != null)
        {
            trigger.Triggered -= tryCast;
            trigger.Disable();
        }
        trigger = newTrigger;

        trigger.Triggered += tryCast;
    }

    public virtual void tryCast()
    {
        if (level > 0)
        {
            if(targeting.GetTarget(transform, range, out Vector2 target)){
                    casting.Cast(transform, target);
            }
        }
    }
    public void quickCast()
    {
        if(targeting.GetTarget(transform, range, out Vector2 target)){
            casting.Cast(transform, target);
        }
    }
    public int getLevel()
    {
        return level;
    }
    public void enable()
    {
        if (level == 0)
        {
            level++;
            trigger.Enable();
        }
        else if(level == 1)
        {
            level++;
            GetComponent<SpellUpgrade>().showUpgradeSelectionMenu(this);
        }
    }
    public void disable()
    {
        trigger.Triggered -= tryCast;
        trigger.Disable();
    }
    public virtual void firstPathEnable(){}
    public virtual void secondPathEnable(){}
}