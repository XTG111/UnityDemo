using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelChoose : MonoBehaviour
{
    [Header("对应关卡信息")] 
    public GameSceneSO gameSceneSo;
    public Vector3 startLoc;
    public bool bClicked = false;
    [Header("监听")]
    public SeceneLoadEventSO LoadEventSo;

    public void OnClicked()
    {
        if (bClicked) return;
        bClicked = true;
        LoadEventSo.RaiseLoadRequestEvent(gameSceneSo,startLoc,true);
    }

}
