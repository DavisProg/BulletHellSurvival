using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public abstract class BaseSpell : MonoBehaviour
{
    [SerializeField] protected int level = 0;
    [SerializeField] protected float range;
    [SerializeField] protected float cooldown;
    public Sprite spellIcon;
    public string spellName;
    public string spellDescription;
    public string firstPathName, firstPathDescription, secondPathName, secondPathDescription;
    [SerializeField] bool addToSpellsKnown = true;
    Dictionary<FieldInfo, object> startingValues = new();

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
            if(targeting.GetTarget(transform, range, out Vector2[] target)){
                foreach(Vector2 targetPos in target)
                {
                    casting.Cast(transform, targetPos);
                } 
            }
        }
    }
    public void quickCast()
    {
        if(targeting.GetTarget(transform, range, out Vector2[] target)){
            foreach(Vector2 targetPos in target)
            {
                casting.Cast(transform, targetPos);
            } 
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
        trigger.Disable();
        level = 0;
        GetComponent<Player>().learntSpells.Remove(this);
        if(startingValues.Count != 0)
        {
            restoreValues();
        }
    }
    public void defineStartingValues()
    {
        foreach (FieldInfo field in GetType().GetFields(
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic))
        {
            if (field.IsDefined(typeof(SerializeField), true))
            {
                startingValues[field] = field.GetValue(this);
            }
        }
    }
    public void restoreValues()
    {
        foreach (var entry in startingValues)
        {
            entry.Key.SetValue(this, entry.Value);
        }
        defineCast();
    }
    public virtual void defineCast() { }
    public virtual void firstPathEnable(){}
    public virtual void secondPathEnable(){}
}