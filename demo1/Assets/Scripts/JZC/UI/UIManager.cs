using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public PlayerStateBar healthBar;
    [Header("监听")]
    public CharacterEventSO HealthEvent;

    private void OnEnable()
    {
        HealthEvent.OnEventRaised += OnHealthEvent;
    }
    private void OnDisable()
    {
        HealthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(PlayerInfo playerInfo)
    {
        var perc = (float)playerInfo.curHp / playerInfo.maxHp;
        healthBar.OnHealthChange(perc);
        //Debug.Log("Damage");
    }
}
