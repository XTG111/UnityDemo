using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour,IInteractable
{
    public SeceneLoadEventSO LoadEventSo;
    public GameSceneSO sceneToGo;
    public Vector3 positionToGo;
    private bool _trigger = false;
    
    public void TriggerAction(GameObject obj)
    {
        if (!_trigger)
        {
            _trigger = true;
            LoadEventSo.RaiseLoadRequestEvent(sceneToGo,positionToGo,true);
        }
    }
}
