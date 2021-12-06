using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableUI : MonoBehaviour
{
    public GameObject UIObject;

    public void ChangeState()
    {
        UIObject.SetActive(!UIObject.activeSelf);
    }
}
