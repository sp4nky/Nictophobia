using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GameObject item = GameController.Instance.playerController.GetForwardObject();
        if (item) PickUp(item);
       
    }

    void PickUp(GameObject item)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowLight LightReference = this.GetComponent<ThrowLight>();
            LightReference.LightQuantity += 1;
            LightReference.HUD.SetHUD(LightReference.LightQuantity);

            GameObject Reference = item;
            Destroy(Reference);
        }
    }
}
