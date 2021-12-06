using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingShake : MonoBehaviour
{
    public Transform cameraTransform;
    [Range(0, 1f)]
    public float shakingValue = 0.1f;
    public float duration = .5f;
    private Vector3 targetPosition;
    private Vector3 defaulPosition;
    public bool shaking { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = cameraTransform.localPosition + Vector3.up * shakingValue;
        defaulPosition = cameraTransform.localPosition;
    }

    public void StartShake()
    {
        if(!shaking) StartCoroutine(Shake());
    }
    public void StopShake()
    {
        shaking = false;
        StopAllCoroutines();
        StartCoroutine(ReturnToDefaultPosition());
    }

    IEnumerator Shake()
    {
        shaking = true;
        float time;
        Vector3 startPosition;
        while (true)
        {
            time = 0;
            startPosition = cameraTransform.localPosition;
            while (time < duration)
            {
                cameraTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            cameraTransform.localPosition = targetPosition;
            UpdatePosition();

        }
    }

    private void UpdatePosition()
    {
        shakingValue *= -1;
        targetPosition = cameraTransform.localPosition + Vector3.up * shakingValue;
    }

    IEnumerator ReturnToDefaultPosition()
    {
        float time = 0;
        Vector3 startPosition = cameraTransform.localPosition;
        while (time < duration)
        {
            cameraTransform.localPosition = Vector3.Lerp(startPosition, defaulPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cameraTransform.localPosition = defaulPosition;
        UpdatePosition();

    }
}
