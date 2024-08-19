using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Attack(int damage, int range)
    {
        this.attackdamage = damage;
        this.attackRange = range;
    }
    [Header("攻击属性")]
    public int attackdamage = 20;
    public float attackRange = 1;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<PlayerInfo>()?.TakeDamage(this);
    }
}
