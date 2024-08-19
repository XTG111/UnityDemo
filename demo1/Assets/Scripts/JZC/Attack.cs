using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("攻击属性")]
    public int attackdamage = 20;
    public float attackRange = 1;

    public bool isBoom;
    public Vector3 lastLoc;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<PlayerInfo>()?.TakeDamage(this);
    }
}
