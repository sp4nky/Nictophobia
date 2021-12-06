using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private bool visibleCursor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = visibleCursor;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetVisible(bool visibleCursor)
    {
        this.visibleCursor = visibleCursor;
    }
}
