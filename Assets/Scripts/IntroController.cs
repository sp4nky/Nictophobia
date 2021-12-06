using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [Header("TurnOn")]
    public GameObject Computer;
    public GameObject Subtitles;
    public GameObject Void;
    public GameObject RTE;

    [Header("PlayAnimation & Sounds")]
    public GameObject Door;
    private Animator DoorAnim;
    public GameObject alarm;

    public SoundBoard SB;

    [Header("TimerSettings")]
    [SerializeField] private float EndTimer;
    [SerializeField] private float StartTimer;

    [Header("CameraShakeSettings")]
    public GameObject MainCamera;




    // Start is called before the first frame update
    void Start()
    {
        StartTimer = 0;
        DoorAnim = Door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTimer <= EndTimer)
        {
            StartTimer += Time.deltaTime * 1;

            if (StartTimer >= EndTimer)
            {
                StartGameEvents();
            }
        }
    }

    public void StartGameEvents()
    {
        StartCoroutine(Shake(7, 3));
        alarm.SetActive(true);


    }

    IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPosition = MainCamera.transform.localPosition;

        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, originalPosition.z);

            if (!SB.source.isPlaying)
            {
                SB.source.Play();
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(LittleShake(5, 3, originalPosition));

    }

    IEnumerator LittleShake (float duration, float magnitude, Vector3 camPosition)
    {
        float elapsed = 0;


        while (elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, camPosition.z);

            if (!SB.source.isPlaying)
            {
                SB.source.Play();
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SmallShake(2, 3, camPosition));
       
    }


    IEnumerator SmallShake (float duration, float magnitude, Vector3 camPosition)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.1f, 0.1f) * magnitude;
            float y = Random.Range(-0.1f, 0.1f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, camPosition.z);

            if (!SB.source.isPlaying)
            {
                SB.source.Play();
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        MainCamera.transform.localPosition = camPosition;
        Computer.SetActive(true);
        Subtitles.SetActive(true);
        Void.SetActive(true);
        RTE.SetActive(true);
        DoorAnim.SetTrigger("Open");
        alarm.SetActive(false);
        SB.source.Stop();
    }


}
