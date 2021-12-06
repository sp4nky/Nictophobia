using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFakeout : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public GameObject pointA;
    [SerializeField] public GameObject pointB;
    [SerializeField] private Transform objectToUse;



    private float startTime;
    private float journeyLength;
    private float distCovered;
    private float fracJourney;

    private void Awake()
    {
        objectToUse.transform.position = pointA.transform.position;
    }


    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(pointA.transform.position, pointB.transform.position);
    }
    void Update()
    {
        distCovered = (Time.time - startTime) * moveSpeed;
        fracJourney = distCovered / journeyLength;
        objectToUse.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, fracJourney);
        if ((Vector3.Distance(objectToUse.position, pointB.transform.position) == 0.0f))
        {
            objectToUse.gameObject.SetActive(false);
        }
    }
}
