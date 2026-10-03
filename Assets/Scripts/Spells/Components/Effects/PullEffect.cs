using System.Collections;
using UnityEngine;
public class PullEffect : IEffect
{
    float strength;
    float duration;
    float pullInterval;
    

    public PullEffect(float strength, float duration, float pullInterval)
    {
        this.strength = strength;
        this.duration = duration;
        this.pullInterval = pullInterval;
    }
    IEnumerator pull(GameObject target, GameObject origin)
    {
        KnockbackController kb = target.GetComponent<KnockbackController>();
        float timePassed = 0;
        while(timePassed < duration)
        {
            if(!origin){
                break;
            }
            Vector2 direction = (Vector2)target.transform.position - (Vector2)origin.transform.position;
            kb.takeKnockback(strength, 0.2f, -direction);
            yield return new WaitForSeconds(pullInterval);
        }
    }
    public void Apply(GameObject target, GameObject origin = default)
    {
        target.GetComponent<Enemy>().StartCoroutine(pull(target, origin));
    }
}
