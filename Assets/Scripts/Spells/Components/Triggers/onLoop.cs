using System;
using System.Collections;
using UnityEngine;

public class onLoop : ITrigger
{
    public event Action Triggered;
    private MonoBehaviour owner;
    public float cooldown { get; set; }
    public float elapsedTime { get; set; } = 0f;
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
            elapsedTime = 0f;
        }
    }
    private IEnumerator Tick()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > cooldown)
            {
                elapsedTime = 0f;
                Triggered?.Invoke();
            }
            yield return null;
        }
    }


}
