using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Image creditsImage;
    public Image controlsImage;
    public Image youWinImage;
    public GameObject[] buttonsToHide;
    public GameObject mainMenu;
    public GameObject youWinMenu;
    public GameObject healthBar;
    public DayNightFSM resetTimer;
    public float newTimer;
    public bool hasKey = false;
    public bool bossBeaten = false;
    public Transform revealedPosition;
    public GameObject[] revealedItem;
    public Text enemieCountText;


    private bool hasBeenRevealed = false;
    private Nest[] nests;
    private Lamp[] lamps;
    private GameObject boss;
    private string minutes;
    private string seconds;
    private bool inPauseMenu = false;
    private bool inLoseMenu = false;

    private void Awake()
    {
       // DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        newTimer = resetTimer.dayTimer;

        buttonsToHide[6].SetActive(false);

        boss = GameObject.FindGameObjectWithTag("Boss");
        

    }

    private void Start()
    {
        buttonsToHide[6].SetActive(false);

        if (boss != null)
        {
            boss.GetComponent<SpriteRenderer>().enabled = false;
            boss.GetComponent<CircleCollider2D>().enabled = false;

        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuOn();
        }

        if (hasKey && bossBeaten)
        {
            WinMenu();
        }


        //instantiate better skellington
        lamps = GameObject.FindObjectsOfType<Lamp>();
        Debug.Log(lamps.Length);
        for (int i = 0; i < lamps.Length; i++)
        {
            if (lamps[i].value != 1)
            {
                Debug.Log(lamps[i]);
                break;
                
            }
            else if (lamps[i].value == 1)
            {
                Debug.Log("All Lamps are on");
                RevealItem();
            }
        }


        nests = GameObject.FindObjectsOfType<Nest>();
        
        if (enemieCountText != null)
        {
            enemieCountText.text = nests.Length.ToString();
        }
        
        boss = GameObject.FindGameObjectWithTag("Boss");


        if (nests.Length == 0)
        {
            if (boss != null)
            {
                boss.GetComponent<SpriteRenderer>().enabled = true;
                boss.GetComponent<Collider2D>().enabled = true;
            }

        }


    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
        resetTimer.inMainMenu = false;
        mainMenu.SetActive(false);
        healthBar.SetActive(true);
        Time.timeScale = 1;
        resetTimer.dayTimer = newTimer;
        resetTimer.isDay = false;

        string minutes = Mathf.Floor(resetTimer.dayTimer / 60).ToString("00");
        string seconds = (resetTimer.dayTimer % 60).ToString("00");
        resetTimer.timerText.text = (minutes + ":" + seconds);

        resetTimer.curState = DayNightFSM.DayNightStates.NIGHT;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        youWinImage.enabled = false;
        buttonsToHide[7].SetActive(false);
        resetTimer.curState = DayNightFSM.DayNightStates.MENU;
        Time.timeScale = 1;
    }


    public void PauseMenuOn()
    {
        mainMenu.SetActive(true);
        buttonsToHide[5].SetActive(true);
        buttonsToHide[6].SetActive(false);
        buttonsToHide[0].SetActive(false);
        healthBar.SetActive(false);
        Time.timeScale = 0;
    }

    public void PauseMenuOff()
    {
        inPauseMenu = true;
        mainMenu.SetActive(false);
        buttonsToHide[0].SetActive(false);
        healthBar.SetActive(true);
        Time.timeScale = 1;
    }

    public void LoseMenu()
    {
        inLoseMenu = true;
        mainMenu.SetActive(true);
        healthBar.SetActive(false);
        buttonsToHide[0].SetActive(false);
        buttonsToHide[5].SetActive(false);
        buttonsToHide[6].SetActive(true);
        Time.timeScale = 0;
    }

    public void WinMenu()
    {
        youWinImage.enabled = true;
        buttonsToHide[7].SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowCredits()
    {
        if (!inLoseMenu)
        {
            buttonsToHide[6].SetActive(false);
        }
        creditsImage.enabled = true;

        buttonsToHide[3].SetActive(true);


    }

    public void HideCredits()
    {

        creditsImage.enabled = false;

        buttonsToHide[3].SetActive(false);


    }

    public void ShowControls()
    {

        controlsImage.enabled = true;

        buttonsToHide[4].SetActive(true);


    }

    public void HideControls()
    {

        controlsImage.enabled = false;

        buttonsToHide[4].SetActive(false);


    }


    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void RevealItem()
    {
        int random = Random.Range(0, 3);

        if (!hasBeenRevealed)
        {
            Instantiate(revealedItem[4], revealedPosition.position, Quaternion.identity);
            hasBeenRevealed = true;
        }

    }
}
