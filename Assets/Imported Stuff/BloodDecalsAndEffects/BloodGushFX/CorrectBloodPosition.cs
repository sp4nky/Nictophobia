using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectBloodPosition : MonoBehaviour
{
    public GameObject NeckGO;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = NeckGO.transform.localPosition;
    }


}
