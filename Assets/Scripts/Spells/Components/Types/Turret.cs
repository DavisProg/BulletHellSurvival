using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [SerializeField] TurretMissile turretCast;
    public void Init(float size, float duration, float damage, float cooldown)
    {
        gameObject.transform.localScale = new Vector3(size, size, 1f);
        turretCast.damage = damage;
        turretCast.newCooldown = cooldown;
        turretCast.defineCast();
        turretCast.enable();
        StartCoroutine(Stay(duration));
    }
    IEnumerator Stay(float duration){
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
