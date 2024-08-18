using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//校验功能的基础移动
public class PawnMove : MonoBehaviour
{
    [Header("监听事件")] 
    public SeceneLoadEventSO LoadEvent; //开始切换场景
    public VoidEventSO afterLoadEvent;//加载之后
    
    [Header("重新开始移动")]
    public float waitTime = 10.0f;
    public bool controlMove = true;
    
    private Rigidbody2D _rigidbody;
    private PlayerPhyCheck _ppcheck;
    public float speed;

    private bool _isFalling;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ppcheck = GetComponent<PlayerPhyCheck>();
    }

    private void FixedUpdate()
    {
        if(_ppcheck.bIsGround && controlMove) Move();
    }

    private void Move()
    {
        Vector2 characterDirx = transform.right;
        Vector2 characterDiry = transform.up;

        var movedir = characterDirx.normalized;

        _rigidbody.velocity = new Vector2(movedir.x * (speed * Time.deltaTime), _rigidbody.velocity.y) ;
    }

    public void SetSpeed(float val)
    {
        speed = val;
    }

    private void OnEnable()
    {
        LoadEvent.LoadRequestEvent += OnLoadEvent;
        afterLoadEvent.OnEventRasied += OnAfterLoadEvent;
    }

    private void OnDisable()
    {
        LoadEvent.LoadRequestEvent -= OnLoadEvent;
        afterLoadEvent.OnEventRasied -= OnAfterLoadEvent;
    }

    private void OnAfterLoadEvent()
    {
        StartCoroutine(ChangeMove());
    }

    IEnumerator ChangeMove()
    {
        yield return new WaitForSeconds(waitTime);
        controlMove = true;
    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        controlMove = false;
    }
}
