using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    #region Normal interact
    public virtual void StartInteract(GameObject interactor)
    {
        print("PeroGenchi");
        SoundBoard sb = interactor.GetComponent<SoundBoard>();
        sb.PlayClip(0);
    }
    public virtual void HoldInteract(GameObject interactor) { }
    public virtual void EndInteract(GameObject interactor) { }
    #endregion
}
