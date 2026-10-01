using System.Collections;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    TMP_Text damageNumber;
    public void Awake()
    {
        damageNumber = gameObject.GetComponent<TMP_Text>();
    }
    public void SetNumber(float damage, float duration)
    {
        damageNumber.text = damage.ToString();
        StartCoroutine(decay(duration));
    }
    private IEnumerator decay(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
