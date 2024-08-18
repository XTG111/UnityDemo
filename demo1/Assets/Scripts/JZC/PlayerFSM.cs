using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;


public enum State {Patrol,Attack,Chase,Dodge}

public class PlayerFSM : MonoBehaviour
{
    private State _curstate = State.Patrol;
    private Rigidbody2D _rigidbody;
    private PlayerInfo _playerInfo;

    public FOV fovIns;
    public PawnMove pawnMove;
    
    //一次最多攻击的敌人数目
    /*public int maxAttackNums = 3;
    private List<Collider2D> _canattackenemy = new List<Collider2D>();*/
    
    [Header("Chase State")]
    public float chasingSpeed = 100.0f;
    public float chasingRotSpeed = 2.0f;
    public float chasingAccuracy = 5.0f;
    
    [Header("Patrol State")]
    public float patrolSpeed = 50.0f;
    public float patrolRotSpeed = 2.0f;
    public float patrolAccuracy = 5.0f;

    [Header("Dodge State")] 
    public float dodgeSpeed = 100.0f;
    public float dodgeCoolDown = 5.0f;
    public float dodgeCoolDownTimer = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        fovIns = GetComponent<FOV>();
        pawnMove = GetComponent<PawnMove>();
        _playerInfo = GetComponent<PlayerInfo>();
    }

    private void Start()
    {
        pawnMove.SetSpeed(patrolSpeed);
    }

    private void Update()
    {
        if (dodgeCoolDownTimer > 0.0f)
        {
            dodgeCoolDownTimer -= Time.deltaTime;
        }
        
        SwitchState();
    }

    private void SwitchState()
    {
        var tempstate = _curstate;

        switch (_curstate)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Dodge:
                Dodge();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Patrol()
    {
        pawnMove.SetSpeed(patrolSpeed);
        if (fovIns.enemyTarget)
        {
            _curstate = State.Chase;
        }
        if(!fovIns.enemyTarget && fovIns.obstacleTaget && dodgeCoolDownTimer <= 0.0f)
        {
            
            _curstate = State.Dodge;
        }
    }

    private void Attack()
    {
        //攻击逻辑需要修改
        if (fovIns.enemyTarget && fovIns.enemyTarget.gameObject)
        {
            Destroy(fovIns.enemyTarget.gameObject);
            fovIns.enemyTarget = null;
        }
    }

    private void Chase()
    {
        pawnMove.SetSpeed(chasingSpeed);
        var distance = Vector2.Distance(
            transform.position,
            fovIns.enemyTarget.gameObject.transform.position
        );
        if (distance <= _playerInfo.attackRange)
        {
            _curstate = State.Attack;
        }
    }

    private void Dodge()
    {
        dodgeCoolDownTimer = dodgeCoolDown;
        pawnMove.SetSpeed(0.0f);
        //障碍物闪避 根据障碍物类型进行选择
        StartCoroutine(PerformDodge());
    }
    IEnumerator PerformDodge()
    {
        float dodgeDuration = 0.5f; // 闪避持续时间
        float elapsedTime = 0f;

        while (elapsedTime < dodgeDuration)
        {
            transform.Translate(Vector2.up * (dodgeSpeed * Time.deltaTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _curstate = State.Patrol;
    }
    /*private void AddObjToCanAttackEnemy()
    {
        var len = maxAttackNums > fovIns.enemyTargets.Count ? fovIns.enemyTargets.Count : maxAttackNums;
        for (var i = 0; i < len; i++)
        {
            _canattackenemy.Add(fovIns.enemyTargets[i]);
        }
    }*/

    public void SetcurState(State s)
    {
        _curstate = s;
    }
}
