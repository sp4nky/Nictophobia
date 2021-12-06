using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReaderUI : MonoBehaviour
{
    public Text title;
    public Text body;

    private void Update()
    {
        bool close = Input.GetButtonDown("Action");
        if (close) Close();
    }

    public void Read(DocumentData document)
    {
        gameObject.SetActive(true);
        title.text = document.title;
        body.text = document.body;
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

}
