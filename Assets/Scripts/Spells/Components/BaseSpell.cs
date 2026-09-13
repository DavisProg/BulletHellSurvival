using UnityEngine.UI;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField] protected int level = 0;
    [SerializeField] protected float range;
    [SerializeField] protected float cooldown;
    public Sprite spellIcon;
    public string spellName;
    public string spellDescription;
    [SerializeField] bool addToSpellsKnown = true;

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
            if (addToSpellsKnown)
            {
                GetComponent<Player>().learntSpells.Add(this);
            }
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
        level = 0;
        GetComponent<Player>().learntSpells.Remove(this);
    }
    public virtual void firstPathEnable(){}
    public virtual void secondPathEnable(){}
}