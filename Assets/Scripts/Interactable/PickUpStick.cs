using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpStick : Interactable
{
    [Header("Count of GlowSticks")]
    public int count = 1;

    public override void StartInteract(GameObject interactor)
    {
        base.StartInteract(interactor);
        interactor.GetComponent<PlayerController>().AddGlowSticks(count);
    }

    public override void EndInteract(GameObject interactor)
    {
        gameObject.SetActive(false);
    }

}
