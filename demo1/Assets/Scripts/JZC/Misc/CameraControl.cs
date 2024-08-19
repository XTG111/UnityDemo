using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PlayerLoop;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D _confiner2D;
    public CinemachineImpulseSource impulseSource;


    [Header("摄像机移动速度")]
    public float panSpeed = 20f;
    private Vector3 dragOrigin;

    [Header("监听切换地图")] 
    public VoidEventSO cameraShakeEvent;
    public VoidEventSO afterLoadScene;
    private void Awake()
    {
        _confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown((1)))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        if (Input.GetMouseButton(1))
        {
            // 计算拖动的距离
            Vector3 difference = dragOrigin - Input.mousePosition;

            // 将摄像机位置根据拖动的距离进行调整
            transform.position += difference * panSpeed * Time.deltaTime;

            // 更新拖动起始点
            dragOrigin = Input.mousePosition;
        }
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
