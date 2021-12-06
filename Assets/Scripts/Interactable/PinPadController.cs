using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class PinPadController : MonoBehaviour
{
    public TextMeshPro textMesh;
    private string pin;
    public string accessPin;

    [Header("Event on activate")]
    public float timeBetweenEvents = 1f;
    public float timeToEnd = 10f;

    public List<UnityEvent> OnButtonOn;

    void Start()
    {
        pin = "____";
        textMesh.text = pin;
        foreach (ConsoleButtonInteraction consoleButton in transform.GetComponentsInChildren<ConsoleButtonInteraction>())
        {
            consoleButton.OnPress += PinCollector;
        }
    }

    public void PinCollector(string number)
    {
        Debug.Log(number);

        if (number == "del")
        {
            pin = "____";
            textMesh.text = pin;

        }
        else
        {
            if (number == "ok")
            {
                if (TryAccess)
                {
                    StopAllCoroutines();
                    StartCoroutine(StartGenerator());
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(IncorrectPin());
                }
            }
            else
            {
                if (pin == "____") pin = "";
                if (pin.Length < 4)
                {
                    pin += number;
                    textMesh.text = pin;
                }
            }
        }

    }

    private bool TryAccess => (pin == accessPin);

    private void OnDisable()
    {
        foreach (ConsoleButtonInteraction consoleButton in transform.GetComponentsInChildren<ConsoleButtonInteraction>())
        {
            consoleButton.OnPress -= PinCollector;
        }
    }

    IEnumerator StartGenerator()
    {
        yield return null;
        pin = "____";
        textMesh.text = "TURNING ON GENERATOR";
        foreach (UnityEvent e in OnButtonOn)
        {
            yield return new WaitForSeconds(timeBetweenEvents);
            e.Invoke();
        }
        yield return new WaitForSeconds(timeToEnd);
        GameController.Instance.Win();
    }

    IEnumerator IncorrectPin()
    {
        yield return null;
        textMesh.text = "INCORRECT PIN";
        yield return new WaitForSeconds(.5f);
        textMesh.text = "";
        yield return new WaitForSeconds(.5f);
        textMesh.text = "INCORRECT PIN";
        yield return new WaitForSeconds(.5f);
        textMesh.text = "";
        yield return new WaitForSeconds(.5f);
        textMesh.text = "INCORRECT PIN";
        yield return new WaitForSeconds(.5f);
        textMesh.text = "";
        yield return new WaitForSeconds(.5f);
        pin = "____";
        textMesh.text = pin;
    }
}
