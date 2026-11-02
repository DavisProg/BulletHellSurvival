using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class DamageAnimationController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool animationPlaying = false;
    private Color startColor = Color.white;
    private event Action finishedAnimation;
    Action colorChange;
    Action colorReset;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void DoDamageAnimation(Color color, float duration = 0.2f)
    {
        float startX;
        float startY;
        if (!animationPlaying)
        {
            animationPlaying = true;
            spriteRenderer.DOColor(color, duration / 2).OnComplete(() =>
            {
                spriteRenderer.DOColor(startColor, duration / 2).OnComplete(() =>
                {
                    animationPlaying = false;
                    finishedAnimation?.Invoke();
                }
                );
            });
            startX = transform.localScale.x;
            startY = transform.localScale.y;
            transform.DOScaleX((float) (startX * 1.4), duration / 3).OnComplete(() =>
            {
                transform.DOScaleX(startX, duration / 2);
            });
            transform.DOScaleY((float) (startY * 0.8), duration / (float)(1.5)).OnComplete(() =>
            {
                transform.DOScaleY(startY, duration / 2);
            });
        }
    }
    public void ChangeColor(Color color, float duration, float animationDuration = 0.1f)
    {
        if(startColor != Color.white)
        {
            return;
        }
        startColor = color;
        if (!animationPlaying)
        {
            DoColorChange(color, animationDuration);
        }
        else{
            colorChange = () => DoColorChange(color, animationDuration);
            finishedAnimation += colorChange;
        }
        StartCoroutine(ColorDecay(duration));
    }
    private void DoColorChange(Color color, float animationDuration)
    {
        animationPlaying = true;
        spriteRenderer.DOColor(color, animationDuration).OnComplete(() =>
        {
            animationPlaying = false;
            if(finishedAnimation?.GetInvocationList().Contains(colorChange) == true)
            {
                finishedAnimation -= colorChange;
            }
        });
    }
    private IEnumerator ColorDecay(float duration)
    {
        yield return new WaitForSeconds(duration);
        startColor = Color.white;
        if (!animationPlaying)
        {
            ResetStartColor();
        }else{
            colorReset = () => ResetStartColor();
            finishedAnimation += colorReset;
        }
    }
    private void ResetStartColor()
    {
        spriteRenderer.color = startColor;
        if(finishedAnimation?.GetInvocationList().Contains(colorReset) == true)
        {
            finishedAnimation -= colorReset;
        }
    }
}
