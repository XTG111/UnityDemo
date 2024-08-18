using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance; 
    
    [Header("人物属性")] 
    public int maxHp = 100;
    public int curHp = 70;

    public int maxShield = 50;
    public int curShield = 0;

    public int attack = 20;
    public float attackRange = 5;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        
        curHp = maxHp-30;
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

}
