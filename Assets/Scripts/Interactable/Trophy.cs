using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : Interactable
{
    public int TrophyCount = 1;
    public override void StartInteract(GameObject interactor)
    {
        base.StartInteract(interactor);
        GameController.Instance.AddToTrophyCollection(TrophyCount);
    }

    public override void EndInteract(GameObject interactor)
    {
        Destroy(gameObject);
    }




}
