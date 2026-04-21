using System.Collections;
using Unity.VisualScripting;
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
        float startingSpeed = enemy.speed;
        enemy.speed -= startingSpeed * reducePercentage;
        yield return new WaitForSeconds(duration);
        enemy.speed = startingSpeed;
    }
    public void Apply(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.StartCoroutine(Slow(enemy));
    }
}
