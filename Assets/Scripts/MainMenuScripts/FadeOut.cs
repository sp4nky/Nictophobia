using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut: MonoBehaviour
{
    public GameObject StartPanel;

    public GameObject BlackOut;
    public GameObject text;

    public GameObject Radio;

    float Timer =10f;

    bool timeEnd;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (BlackOut.activeInHierarchy == true && !timeEnd)
        {
            Timer -= Time.deltaTime;
            print(Timer);


            if (Timer <=0)
            {
                Radio.SetActive(true);
                BlackOut.SetActive(false);
                text.SetActive(false);

                StartPanel.SetActive(true);
                timeEnd = true;
                Timer = 10f;
            }
        }
    }
}
