using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEnd : Interactable
{
    public Animator anim;

    private bool secondEnd;

    public override void StartInteract(GameObject interactor)
    {
        base.StartInteract(interactor);
        if(anim.GetBool("Active"))
        {
            anim.SetBool("Active", false);
        }
        else
        {
            anim.SetBool("Active", true);
            if (!secondEnd)
            {
                GameController.Instance.ActiveSecondEnd();
                secondEnd = true;
            }
        }
    }

}
