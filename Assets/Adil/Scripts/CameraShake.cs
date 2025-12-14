using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    public Transform camTransform;

    public float defaultShakeDuration = 0.15f;
    public float shakeMagnitude = 0.2f;

    private Vector3 initialPosition;

    void Awake()
    {
        if (Instance == null) Instance = this;

        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        initialPosition = camTransform.localPosition;
    }

    public void TriggerShake()
    {
        TriggerShake(defaultShakeDuration);
    }

    public void TriggerShake(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(duration));
    }

    IEnumerator Shake(float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 randomPoint = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            camTransform.localPosition = new Vector3(randomPoint.x, randomPoint.y, initialPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        camTransform.localPosition = initialPosition;
    }
}
