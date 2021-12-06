using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletInteraction : Interactable
{
    public DocumentData document;
    public ReaderUI readerUI;

    public override void StartInteract(GameObject interactor)
    {
        if (!document) return;
        base.StartInteract(interactor);
        readerUI.Read(document);
    }
}
