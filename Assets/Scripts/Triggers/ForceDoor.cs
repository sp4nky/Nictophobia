using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class ForceDoor : Interactable
{
    private Animator anim;
    private Collider col;
    private AudioSource sound;
    private PlayerController interactorController;

    public GameObject sparksEffectParent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        sound = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        anim.Rebind();
        anim.SetTrigger("open");
    }

    public void CloseDoor() => anim.SetTrigger("close");

    public override void StartInteract(GameObject interactor)
    {
        base.StartInteract(interactor);
        OpenDoor();
        this.interactorController = interactor.GetComponent<PlayerController>();
    }

    public override void EndInteract(GameObject interactor)
    {
        this.interactorController = interactor.GetComponent<PlayerController>();
    }

    #region Used by animation door
    public void EnablePlayerMovement() => interactorController.EnableMovement();

    public void DisablePlayerMovement() => interactorController.DisableMovement();

    public void PlaySoundEffect() => sound.Play();

    public void PlaySparksEffect()
    {
        foreach(ParticleSystem spark in sparksEffectParent.GetComponentsInChildren<ParticleSystem>())
        {
            spark.Play();
        }
    }

    public void DisableForceDoor()
    {
        col.enabled = false;
    }
    #endregion

}
