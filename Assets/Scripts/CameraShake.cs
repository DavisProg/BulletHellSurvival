using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private bool start;
    [SerializeField] private float duration;
    [SerializeField] private float intensity;
    [SerializeField] private AnimationCurve curve;

    public void startCameraShake()
    {
        StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        float elapsedTime = 0;
        Vector3 startPosition = new Vector3(0, 0, 0);
        while(elapsedTime < duration)
        {
            startPosition = transform.position;
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength * intensity;
            yield return null;
        }
        transform.position = startPosition;
    }
}
