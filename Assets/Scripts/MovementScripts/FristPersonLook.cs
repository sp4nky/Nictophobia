using UnityEngine;

public class FristPersonLook : MonoBehaviour
{
    [Range(1f, 10f)]
    public float mouseSensibility = 1;

    public Transform firstPersonCameraTransform;

    private Vector2 rotationY = Vector2.zero;
    private Vector2 rotationX = Vector2.zero;
    private bool canMove;
    private void Start()
    {
        canMove = true;
    }

    void Update()
    {
        if(canMove) MouseLook();

    }

    private void MouseLook()
    {
        float horizontalLook = Input.GetAxis("Mouse X");
        float verticalLook = Input.GetAxis("Mouse Y");

        rotationX.x += -verticalLook;
        rotationY.y += horizontalLook;
        transform.localEulerAngles = rotationY * mouseSensibility;
        if (Mathf.Abs(rotationX.x * mouseSensibility) < 90)
            firstPersonCameraTransform.localEulerAngles = rotationX * mouseSensibility;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }
}
