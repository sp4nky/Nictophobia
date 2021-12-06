using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorInteraction : Interactable
{
    public GameObject button;
    public Material buttonOn;

    public float timeBetweenEvents = 1f;
    public float timeToEnd = 10f;

    public List<UnityEvent> OnButtonOn;

    public override void StartInteract(GameObject interactor)
    {
        base.StartInteract(interactor);
        StartCoroutine(StartGenerator());
    }
    
    IEnumerator StartGenerator()
    {
        yield return null;
        MeshRenderer meshButton= button.GetComponent<MeshRenderer>();
        meshButton.material = buttonOn;
        foreach (UnityEvent e in OnButtonOn)
        {
            yield return new WaitForSeconds(timeBetweenEvents);
            e.Invoke();
        }
        yield return new WaitForSeconds(timeToEnd);
        GameController.Instance.Win();

    }
}
