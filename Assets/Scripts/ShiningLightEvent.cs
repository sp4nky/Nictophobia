using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningLightEvent : MonoBehaviour
{
    [Header("CameraShakeSettings")]
    public GameObject MainCamera;

    [Header("SoundSettings")]
    public SoundBoard SB;

    [Header("GameObject")]
    public GameObject EventGO;
    public GameObject Meteor;


    private void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Meteor.SetActive(true);
            SB.source.Play();
            StartCoroutine(SmallShake(7, 1));
        }
    }




    IEnumerator SmallShake(float duration, float magnitude)
    {
        Vector3 originalPosition = MainCamera.transform.localPosition;

        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        StartCoroutine(BiggerShake(7, 1, originalPosition));
    }

    IEnumerator BiggerShake(float duration, float magnitude, Vector3 originalPosition)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.7f, 0.7f) * magnitude;
            float y = Random.Range(-0.7f, 0.7f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        StartCoroutine(BiggestShake(5, 1, originalPosition));

    }

    IEnumerator BiggestShake(float duration, float magnitude, Vector3 originalPosition)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        StartCoroutine(SmallerShake(5, 1, originalPosition));

    }

    IEnumerator SmallerShake(float duration, float magnitude, Vector3 originalPosition)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.4f, 0.4f) * magnitude;
            float y = Random.Range(-0.4f, 0.4f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        StartCoroutine(SmallestShake(4, 1, originalPosition));

    }

    IEnumerator SmallestShake(float duration, float magnitude, Vector3 originalPosition)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.1f, 0.1f) * magnitude;
            float y = Random.Range(-0.1f, 0.1f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        MainCamera.transform.localPosition = originalPosition;

    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(EventGO);
    }

}
