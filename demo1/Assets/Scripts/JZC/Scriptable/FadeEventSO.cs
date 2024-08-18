using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float, bool> OnEventRaised;

    //逐渐变黑
    public void FadeIn(float duration)
    {
        RaiseEvent(Color.black, duration,true);
    }

    //逐渐消失
    public void FadeOut(float duration)
    {
        RaiseEvent(Color.clear, duration,false);
    }

    public void RaiseEvent(Color target, float duration, bool fadein)
    {
        OnEventRaised?.Invoke(target, duration, fadein);
    }
}
