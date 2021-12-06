using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioInteraction : Interactable
{
    private AudioSource AS;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();

    }

    public override void StartInteract(GameObject interactor)
    {
        base.StartInteract(interactor);
        if (AS.isPlaying)
        {
            AS.Stop();
        }
        else
        {
            AS.Play();
        }
    }


}
