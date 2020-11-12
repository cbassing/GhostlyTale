using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DayNightFSM : MonoBehaviour
{
    [SerializeField]
    public enum DayNightStates
    {
        DAY,
        NIGHT,

        MENU
    }

    public DayNightStates curState;
    public GameObject player;
    public float dayTimer;
    public bool isDay;
    public bool inMainMenu = true;
    public Text timerText;
    public GameManager gm;

    private Dictionary<DayNightStates, Action> fsm = new Dictionary<DayNightStates, Action>();
    private IKillable killable;
    private string minutes;
    private string seconds;
    
    // Start is called before the first frame update
    void Start()
    {
        fsm.Add(DayNightStates.DAY, new Action(DayState));
        fsm.Add(DayNightStates.NIGHT, new Action(NightState));
        fsm.Add(DayNightStates.MENU, new Action(MenuState));

        SetState(DayNightStates.MENU);

       
    }

    // Update is called once per frame
    void Update()
    {
        fsm[curState].Invoke();
    }

    public void DayState()
    {
        isDay = true;

        if (isDay)
        {
            if (player.gameObject != null)
            {
                gm.LoseMenu();

            }

        }


        // if isDay
        // trigger lose condition

    }

    public void NightState()
    {

        
        if (dayTimer >= 0)
        {
            dayTimer -= Time.deltaTime;
        }

        if (dayTimer <= 0)
        {
            SetState(DayNightStates.DAY);
        }
        string minutes = Mathf.Floor(dayTimer / 60).ToString("00");
        string seconds = (dayTimer % 60).ToString("00");
        
        timerText.text = (minutes + ":" + seconds);
        

    }

    public void MenuState()
    {
        if (!inMainMenu)
        {
            SetState(DayNightStates.NIGHT);
        }
    }


    public void SetState(DayNightStates newState)
    {
        curState = newState;
    }


}
