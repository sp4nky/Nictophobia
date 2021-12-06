using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController playerController { get; set; }
    public UIController ui;
    //public PostProSettings postPro;
    private MainMenuController menu;
    private MouseController mouseController;
    private static GameController _instance;


    public GameObject MoveDoor;

   private int TrophyCount =0;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        TrophyCount = 0;
        DontDestroyOnLoad(gameObject);
        menu = GetComponent<MainMenuController>();
        mouseController = GetComponent<MouseController>();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {

    }

    public void Die()
    {
        Debug.Log("PlayerDeath");
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        //postPro.ChangeToDeathProfile();
        yield return new WaitForSeconds(.5f);
        playerController.DisableMovement();
        ui.EnableDeathScreen();
        mouseController.SetVisible(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        Debug.Log("Win");
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(3);
        ui.EnableWinScreen();
        Time.timeScale = 0;
        yield return new WaitForSeconds(10);
        menu.GoToCredits();
    }

    public void GoToMainMenu()
    {
        menu.GoToTitleScreen();
    }

    public void AddGlowSticksPlayer(int count)
    {
        playerController.AddGlowSticks(count);
    }

    public void Exit()
    {
        menu.QuitGame();
    }

    public void PauseGame()
    {
        playerController.DisableMovement();
        mouseController.SetVisible(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        mouseController.SetVisible(false);
        playerController.EnableMovement();

    }

    public void AddToTrophyCollection(int Amount)
    {
        TrophyCount += Amount;
    }

    public void ActiveSecondEnd()
    {
        if (TrophyCount == 5)
        {
            Destroy(MoveDoor);
            TrophyCount += 1;
            Debug.Log("FinalActived");
        }
        else
            Debug.Log("missing trophies " + TrophyCount);
    }
}
