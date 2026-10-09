using System.Collections;
using UnityEngine;

public class SlowEffect : IEffect
{
    float reducePercentage;
    float duration;

    public SlowEffect(float reducePercentage, float duration)
    {
        this.reducePercentage = reducePercentage / 100;
        this.duration = duration;
    }
    IEnumerator Slow(Enemy enemy)
    {
        float resultDifference = enemy.maxSpeed * reducePercentage;
        enemy.speed -= resultDifference;
        yield return new WaitForSeconds(duration);
        enemy.speed += resultDifference;
    }
    public void Apply(GameObject target, GameObject origin = default)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.StartCoroutine(Slow(enemy));
    }
}
