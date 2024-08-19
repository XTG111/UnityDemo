using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<PlayerInfo> OnEventRaised;

    public void RaiseEvent(PlayerInfo playerInfo)
    {
        OnEventRaised?.Invoke(playerInfo);
    }
}
