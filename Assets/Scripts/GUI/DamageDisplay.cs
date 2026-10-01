using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class DamageDisplay : MonoBehaviour
{
    TMP_Text damageNumber;
    [SerializeField] float animationStrength;
    public void Awake()
    {
        damageNumber = gameObject.GetComponent<TMP_Text>();
    }
    public void SetNumber(float damage, float duration)
    {
        damageNumber.text = damage.ToString();
        float startY = transform.position.y;
        transform.DOMoveY(startY + animationStrength, duration / 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveY(startY, duration / 2).SetEase(Ease.Linear).OnComplete(() =>{Destroy(gameObject);});
        }
        ); 
    }
}
