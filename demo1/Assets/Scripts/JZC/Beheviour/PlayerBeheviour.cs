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
    [Header("当前能力")] 
    public BehaviourState curState = BehaviourState.BoomPlayer;
    public BoomState bst = BoomState.Init_NoDamage;
    public SpeedState sst = SpeedState.Init_FastSpeed;

    private void Awake()
    {
        _playerPhyCheck = GetComponent<PlayerPhyCheck>();
        _playerInfo = GetComponent<PlayerInfo>();
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
            bst = BoomState.Seco_TakeDamage;
        }
    }

    public void SecoDamage()
    {
        var time = 0.0f;
        while (time < _playerPhyCheck.debuffTime)
        {
            time += Time.deltaTime;
            if (time >= _playerPhyCheck.debuffTime)
            {
                bst = BoomState.Init_NoDamage;
            }
        }
    }
    
    private void DoSpeed()
    {
        switch (sst)
        {
            case SpeedState.Init_FastSpeed:
                break;
            case SpeedState.Seco_LowSpeed:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
