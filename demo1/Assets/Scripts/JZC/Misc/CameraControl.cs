using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D _confiner2D;
    public CinemachineImpulseSource impulseSource;

    

    [Header("监听切换地图")] 
    public VoidEventSO cameraShakeEvent;
    public VoidEventSO afterLoadScene;
    private void Awake()
    {
        _confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        cameraShakeEvent.OnEventRasied += OnCameraShakeEvent;
        afterLoadScene.OnEventRasied += OnAfterSceneLoaded;
    }
    private void OnDisable()
    {
        cameraShakeEvent.OnEventRasied -= OnCameraShakeEvent;
        afterLoadScene.OnEventRasied -= OnAfterSceneLoaded;
    }

    private void OnAfterSceneLoaded()
    {
        GetNewCameraBouns();
    }

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    /*private void Start()
    {
        
    }*/

    //获取每个场景的限制框，需要在场景中绘制然后添加标签
    private void GetNewCameraBouns()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null) return;
        _confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        _confiner2D.InvalidateCache();
    }
}
