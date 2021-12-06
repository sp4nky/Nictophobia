using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    private PlayerController PC;
    public GameObject EventGO;

    // Start is called before the first frame update
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Hello There");
        if (other.tag == "Player")
        {
            EventGO.SetActive(true);


            Destroy(gameObject);
        }
    }

}
