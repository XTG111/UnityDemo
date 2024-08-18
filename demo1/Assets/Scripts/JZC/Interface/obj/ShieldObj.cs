using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObj : MonoBehaviour, IInteractable
{
    public int shielddel = 10;
    private bool _trigger = false;

    public void TriggerAction(GameObject obj)
    {
        if (!_trigger)
        {
            _trigger = true;
            obj.GetComponent<PlayerInfo>()?.SetcurShield(shielddel);
            Destroy(gameObject);
        }
    }
}
