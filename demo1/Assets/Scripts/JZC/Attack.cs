using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("攻击属性")]
    public int attackdamage = 20;
    public float attackRange = 1;

    public bool underTrigger = false;
    public bool isBoom;
    public Vector3 lastLoc;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!underTrigger)
        {
            underTrigger = true;
            if(other.GetComponent<Lurker_Trap>() == null ) other.GetComponent<PlayerInfo>()?.TakeDamage(this);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        underTrigger = false;
    }
}
