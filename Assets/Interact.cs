using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("Inputs to Interact")]
    public string action = "Action";

    private bool inputActionDown;
    private bool inputActionHold;
    private bool inputActionUp;

    void Update()
    {
        inputActionDown = Input.GetButtonDown(action);
        inputActionHold = Input.GetButton(action);
        inputActionUp = Input.GetButtonUp(action);

        GameObject item = GameController.Instance.playerController.GetForwardObject();
        if (item)
        {
            Interactable interactObject = item.GetComponent<Interactable>();
            if(interactObject)
                InteractionActionButton(interactObject);
        }
    }

    private void InteractionActionButton(Interactable interactObject)
    {
        if (inputActionDown)
            interactObject.StartInteract(gameObject);
        if (inputActionHold)
            interactObject.HoldInteract(gameObject);
        if (inputActionUp)
            interactObject.EndInteract(gameObject);
    }  

    
}
