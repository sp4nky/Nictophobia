using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text Amount;
    public Image hand;
    public GameObject deathScreen;
    public GameObject winScreen;
    public GameObject pauseScreen;
    [Header("Sprites")]
    public Sprite HandObstacleOpen;
    public Sprite HandObstacleClose;
    public Sprite HandStick;
    public Sprite forceDoor;

    private void Start()
    {
        hand.enabled = false;
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        deathScreen.SetActive(false);
    }

    private void Update()
    {
        UpdateHud();
    }

    private void UpdateHud()
    {
        bool action = Input.GetButtonDown("Fire1");
        string tagForwardObject = GameController.Instance.playerController.GetTagForwardObject();
        switch (tagForwardObject)
        {
            case "Door":
                EnableForceDoor();
                break;
            case "Box":
                if (action) 
                    EnableHandObstacleClose();
                else EnableHandObstacleOpen();
                break;
            case "Stick":
                EnableHandStick();
                break;
            case "Other":
                EnableHandStick();
                break;
            default:
                hand.enabled = false;
                break;
        }
    }

    public void SetHUD(int Quantity)
    {
        if(Amount.transform.parent.gameObject.activeSelf)
            Amount.text = Quantity.ToString();
    }

    public void EnableHandObstacleOpen()
    {
        hand.enabled = true;
        hand.sprite = HandObstacleOpen;

    }    
    
    public void EnableForceDoor()
    {
        hand.enabled = true;
        hand.sprite = forceDoor;

    }

    public void EnableHandObstacleClose()
    {
        hand.enabled = true;
        hand.sprite = HandObstacleClose;
    }

    public void EnableHandStick()
    {
        hand.enabled = true;
        hand.sprite = HandStick;
    }

    public void EnableDeathScreen()
    {
        deathScreen.SetActive(true);
    }    
    
    public void EnableWinScreen()
    {
        winScreen.SetActive(true);
    }


}
