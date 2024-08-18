using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureObj : MonoBehaviour,IInteractable
{
    public int curedel = 10;
    private bool _trigger = false;
    public void TriggerAction(GameObject obj)
    {
        if (!_trigger)
        {
            _trigger = true;
            obj.GetComponent<PlayerInfo>()?.SetcurHp(curedel);
            Destroy(gameObject);
        }
        
    }
}
