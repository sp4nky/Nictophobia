using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareGlowStick : Interactable
{
    public GameObject scareObject;
    public float scareDuration = 2f;
    private SoundBoard soundBoard;
    private bool scaring;
    
    [Header("Count of GlowSticks")]
    public int count = 1;
    private void Awake()
    {
        soundBoard = GetComponent<SoundBoard>();
    }

    public override void StartInteract(GameObject interactor)
    {
        if (scaring)
            return;
        base.StartInteract(interactor);
    }

    public override void EndInteract(GameObject interactor)
    {
        if (scaring) 
            return;
        scaring = true;
        interactor.GetComponent<PlayerController>().AddGlowSticks(count);
        scareObject.SetActive(true);
        var playerForward = interactor.transform.forward;
        scareObject.transform.forward = playerForward.normalized;
        scareObject.transform.position += playerForward.normalized * -8f;
        soundBoard.PlayClip(0);
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(scareDuration);
        Destroy(gameObject);
    }

}
