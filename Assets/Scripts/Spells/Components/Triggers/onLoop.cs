using System;
using System.Collections;
using UnityEngine;

public class onLoop : ITrigger
{
    public event Action Triggered;
    private MonoBehaviour owner;
    private float cooldown;
    private Coroutine routine;

    public onLoop(MonoBehaviour owner, float cooldown)
    {
        this.owner = owner;
        this.cooldown = cooldown;
    }

    public void Enable()
    {
        if (routine == null)
        {
            routine = owner.StartCoroutine(Tick());
        }
    }
    public void Disable()
    {
        if (routine != null)
        {
            owner.StopCoroutine(routine);
            routine = null;
        }
    }
    private IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            Triggered?.Invoke();
        }
    }


}
