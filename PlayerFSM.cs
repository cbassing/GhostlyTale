using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerFSM : MonoBehaviour, IKillable
{
    [SerializeField]
    public enum PlayerStates
    {
        SKELETON,
        SPIRIT
    }
    [SerializeField]
    int damageAmount;
    [SerializeField]
    int maxHealth;
    
    
    private SkeletonController skeletonController;
    private SpiritController spiritController;
    private EmptySkeleton empty;
    private Dictionary<PlayerStates, Action> fsm = new Dictionary<PlayerStates, Action>();
    private GameObject[] boxes;
    private GameObject[] platforms;
    private GameObject[] enemies;
    private IKillable destroy;

    public GameObject pauseMenu;
    public PlayerHealth playerHealth;
    public Animator anim = null;
    public PlayerStates curState;
    public GameObject[] emptySkeleton = null;
    public GameObject[] skeletonGO;
    public GameObject spiritGO;
    public int newSkeletonID;
    public bool canPossess = false;
    public bool didPossess = false;
    public bool isDead = false;
    void Awake()
    {
        skeletonController = GetComponent<SkeletonController>();
        spiritController = GetComponent<SpiritController>();

        boxes = GameObject.FindGameObjectsWithTag("Box");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //health = playerHealth.health;
    }
    // Start is called before the first frame update
    void Start()
    {
        fsm.Add(PlayerStates.SPIRIT, new Action(SpiritState));
        fsm.Add(PlayerStates.SKELETON, new Action(SkeletonState));

        SetState(PlayerStates.SPIRIT);
    }

    // Update is called once per frame
    void Update()
    {
        fsm[curState].Invoke();
    }


    void SpiritState()
    {
        playerHealth.isDead = false;
        playerHealth.currentHealth = maxHealth;
        skeletonController.enabled = false;
        skeletonGO[newSkeletonID].SetActive(false);
        spiritController.enabled = true;
        spiritGO.SetActive(true);
        spiritController.rb2d.gravityScale = 0;

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].GetComponent<Collider2D>().isTrigger = true;
            boxes[i].GetComponent<Rigidbody2D>().isKinematic = true;
            boxes[i].GetComponent<Rigidbody2D>().simulated = false;
        }


        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<EnemyFSM>().SetState(EnemyFSM.EnemyStates.IDLE);
                enemies[i].GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                break;
            }
        }

        if (canPossess && Input.GetKeyDown(KeyCode.Keypad0))
        {
            //Debug.Log("P was pressed");
            SetState(PlayerStates.SKELETON);

            didPossess = true;
            if (destroy != null && didPossess)
            {
                destroy.Destroyer();
            }

            canPossess = false;
        }


       

    }

    void SkeletonState()
    {

        spiritController.enabled = false;
        spiritGO.SetActive(false);
        skeletonController.enabled = true;
        skeletonGO[newSkeletonID].SetActive(true);
        skeletonController.rb.gravityScale = 1;


        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].GetComponent<Collider2D>().isTrigger = false;
            boxes[i].GetComponent<Rigidbody2D>().isKinematic = false;
            boxes[i].GetComponent<Rigidbody2D>().simulated = true;
        }



        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("O was pressed");
            SetState(PlayerStates.SPIRIT);
            Instantiate(emptySkeleton[newSkeletonID], transform.position, Quaternion.identity);

        }

        bool skeletonDestroyed = playerHealth.isDead;

        if (skeletonDestroyed)
        {
            SetState(PlayerStates.SPIRIT);
            
        }

    }


    void SetState(PlayerStates newState)
    {
        curState = newState;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        destroy = other.GetComponent<IKillable>();
        empty = other.GetComponent<EmptySkeleton>();

        if (curState == PlayerStates.SPIRIT && empty != null)
        {
            canPossess = true;
            newSkeletonID = empty.skeletonID;
            skeletonController.CurrentSkeletonID = newSkeletonID;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        empty = other.GetComponent<EmptySkeleton>();
        if (curState == PlayerStates.SPIRIT && empty != null)
        {
            canPossess = false;
            didPossess = false;
        }
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }


}
