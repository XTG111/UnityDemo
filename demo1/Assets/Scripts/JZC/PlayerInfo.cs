using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance; 
    
    [Header("人物属性")] 
    public int maxHp = 100;
    public int curHp = 70;

    public int maxShield = 50;
    public int curShield = 0;

    [Header("免伤")] 
    public float invulnerableDuration = 5.0f;
    private float _invulnerableCount = 0.0f;
    public bool invulnerable;

    [Header("事件")] 
    public UnityEvent<Transform> OnTackDamge;
    public UnityEvent OnDeath;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        
        curHp = maxHp;
    }

    private void Update()
    {
        if (invulnerable)
        {
            _invulnerableCount -= Time.deltaTime;
            if (_invulnerableCount <= 0.0f)
            {
                invulnerable = false;
            }
        }
    }

    public void SetcurHp(int del)
    {
        if (del < 0)
        {
            del = math.clamp(del+curShield,del,0);
        }
        curHp = math.clamp(curHp + del, 0, maxHp);
    }
    
    public void SetcurShield(int del)
    {
        curShield = math.clamp(curShield + del, 0, maxShield);
    }

    public int GetCurHP()
    {
        return curHp;
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable) return;
        SetcurHp(-attacker.attackdamage);
        if (curHp > 0)
        {
            TriggerInvulnerable();
            OnTackDamge?.Invoke(attacker.transform);
        }
        else
        {
            //TODO : Die
            OnDeath?.Invoke();
        }
    }

    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            _invulnerableCount = invulnerableDuration;
        }
    }
}
