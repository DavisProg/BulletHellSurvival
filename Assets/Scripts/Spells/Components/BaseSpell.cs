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

    protected ITargetable targeting;
    protected ICastable casting;

    bool canCast = true;

    protected IEnumerator tryCast()
    {
        if (level > 0)
        {
            Debug.Log("Hello");
            while (canCast)
            {
                if(targeting.GetTarget(transform, range, out Vector2 target)){
                    casting.Cast(transform, target);
                }
                canCast = false;
                yield return new WaitForSeconds(cooldown);
                canCast = true;
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
            StartCoroutine(tryCast());
        }
        else if(level == 1)
        {
            level++;
            GetComponent<SpellUpgrade>().showUpgradeSelectionMenu(this);
        }
    }
    public virtual void firstPathEnable(){}
    public virtual void secondPathEnable(){}
}