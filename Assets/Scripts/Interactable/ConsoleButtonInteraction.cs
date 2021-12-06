using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleButtonInteraction : Interactable
{
    [Header("Number to press")]
    public string number;
    public delegate void Contact(string number);
    public event Contact OnPress;

    public override void StartInteract(GameObject interactor)
    {
        base.StartInteract(interactor);
        OnPress.Invoke(number);
    }

}
