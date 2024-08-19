using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BehaviourState
{
    BoomPlayer,
    SpeedPlayer
}

public enum BoomState
{
    Init_NoDamage,
    Seco_TakeDamage
}

public enum SpeedState
{
    Init_FastSpeed,
    Seco_LowSpeed
}

public class PlayerBeheviour : MonoBehaviour
{
    private PlayerPhyCheck _playerPhyCheck;
    private PlayerInfo _playerInfo;
    private PlayerFSM _playerFsm;
    [Header("当前能力")] 
    public BehaviourState curState = BehaviourState.BoomPlayer;
    public BoomState bst = BoomState.Init_NoDamage;
    public SpeedState sst = SpeedState.Init_FastSpeed;

    [Header("level2 速度")] 
    public float fastSpeed = 1.0f;
    public float lowSpeed = 1.0f;
    private bool change = false;
    private float timeRemaining = 0.0f;
    private bool isActive = false;
    
    private void Awake()
    {
        _playerPhyCheck = GetComponent<PlayerPhyCheck>();
        _playerInfo = GetComponent<PlayerInfo>();
        _playerFsm = GetComponent<PlayerFSM>();
    }

    public void ChangeState()
    {
        if (curState == BehaviourState.BoomPlayer)
        {
            curState = BehaviourState.SpeedPlayer;
        }
        else
        {
            curState = BehaviourState.BoomPlayer;
        }
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (curState == BehaviourState.BoomPlayer)
        {
            //
            DoBoom();
        }
        else
        {
            //
            DoSpeed();
        }
    }

    private void DoBoom()
    {
        switch (bst)
        {
            case BoomState.Init_NoDamage:
                NoDamage();
                break;
            case BoomState.Seco_TakeDamage:
                SecoDamage();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void NoDamage()
    {
        //触发器通知
        if (_playerPhyCheck.underEmo)
        {
            _playerPhyCheck.underEmo = false;
            timeRemaining = _playerPhyCheck.debuffTime;
            isActive = true;
            _playerPhyCheck.bInEmo = false;
            bst = BoomState.Seco_TakeDamage;
        }
    }

    public void SecoDamage()
    {
        if (isActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0.0f)
            {
                bst = BoomState.Init_NoDamage;
                return;
            }
        }
    }
    
    private void DoSpeed()
    {
        switch (sst)
        {
            case SpeedState.Init_FastSpeed:
                FastSpeed();
                break;
            case SpeedState.Seco_LowSpeed:
                LowSpeed();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void FastSpeed()
    {
        /*if (!change)
        {
            change = true;
            
        }*/
        if (_playerPhyCheck.underDici)
        {
            _playerFsm.patrolSpeed *= lowSpeed;
            change = false;
            sst = SpeedState.Seco_LowSpeed;
        }
        //Debug.Log("speed: "+GetComponent<Rigidbody2D>().velocity.x);
    }

    public void LowSpeed()
    {
        /*if (!change)
        {
            change = true;
            _playerFsm.patrolSpeed *= lowSpeed;
        }*/
        
        if (!_playerPhyCheck.underDici)
        {
            _playerFsm.patrolSpeed *= fastSpeed;
            change = false;
            sst = SpeedState.Init_FastSpeed;
        }
        //Debug.Log("speed: "+GetComponent<Rigidbody2D>().velocity.x);
    }
}
