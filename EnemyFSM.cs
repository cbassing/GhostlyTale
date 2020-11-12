using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField]
    public enum EnemyStates
    {
        IDLE,
        ATTACK,
        PURSUIT
    }
    private Enemy enemy;
    private Dictionary<EnemyStates, Action> fsm = new Dictionary<EnemyStates, Action>();
    private SkeletonController skeleton;
    private bool isSkeleton = false;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public KillBox killBox;
    public Animator anim = null;
    public EnemyStates curState;
    public Transform idlePosition;
    public Transform playerPosition;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        fsm.Add(EnemyStates.IDLE, new Action(IdleState));
        fsm.Add(EnemyStates.ATTACK, new Action(AttackState));
        fsm.Add(EnemyStates.PURSUIT, new Action(PursuitState));

        SetState(EnemyStates.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        fsm[curState].Invoke();
        isSkeleton = GameObject.FindGameObjectWithTag("Skeleton");
    }

    public void IdleState()
    {
        if (playerPosition.gameObject != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, idlePosition.position, enemy.speed);
            Vector3 distance = idlePosition.transform.localPosition - transform.localPosition;

            if (distance.x >= 0)
            {
                sprite.flipX = true;
            }


            if (distance.x <= 10 || distance.y < 10)
            {
                if (killBox.canFollow)
                SetState(EnemyStates.PURSUIT);
            }
        }

    }
    public void AttackState()
    {
        if (playerPosition.gameObject != null)
        {
            Vector3 distance = playerPosition.transform.localPosition - transform.localPosition;


            if (distance.x > 0)
            {
                sprite.flipX = false;
            }
            else if (distance.x < 0)
            {
                sprite.flipX = true;
            }

            if (Mathf.Abs(distance.x) < 1f)
            {
                anim.SetTrigger("attack");
            }
            else if (Mathf.Abs(distance.x) > .5f)
            {
                SetState(EnemyStates.PURSUIT);
            }

        }

    }


    public void PursuitState()
    {
        if (playerPosition.gameObject != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, enemy.speed);

            Vector3 direction = playerPosition.transform.localPosition - transform.localPosition;
            //Debug.Log(direction);
            if (direction.x > 0)
            {
                sprite.flipX = false;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = true;
            }

            if (Mathf.Abs(direction.x) < 2f)
            {
                SetState(EnemyStates.ATTACK);
            }

            if (Mathf.Abs(direction.x) > 10f || !isSkeleton)
            {
                SetState(EnemyStates.IDLE);

                direction = idlePosition.transform.localPosition - transform.localPosition;

                if (direction.x >= 0)
                {
                    sprite.flipX = true;
                }
            }

        }

    }


    public void SetState(EnemyStates newState)
    {
        curState = newState;
    }


}
