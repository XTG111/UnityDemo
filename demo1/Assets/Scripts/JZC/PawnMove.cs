using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [Header("反弹")] 
    public bool isHurt;
    public float hurtForce;
    [Header("Jump")] 
    public float jumpSpeedx = 2.0f;
    public float jumpForce = 7.0f;
    
    public bool isDead;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ppcheck = GetComponent<PlayerPhyCheck>();
    }

    private void FixedUpdate()
    {
        if(_ppcheck.bIsGround && controlMove && !isHurt && !isDead) Move();
    }

    private void Move()
    {
        Vector2 characterDirx = transform.right;
        Vector2 characterDiry = transform.up;

        var movedir = characterDirx.normalized;

        _rigidbody.velocity = new Vector2(movedir.x * transform.localScale.x * (speed * Time.deltaTime), _rigidbody.velocity.y) ;
        //Debug.Log("speed"+movedir.x * (speed * Time.deltaTime));
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0); //jumpSpeedx
        _ppcheck.isFalling = false;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        
        /*Debug.Log("_rd.velocity.x: "+_rigidbody.velocity.x);
        _rigidbody.velocity = new Vector2( jumpSpeedx, 0);*/
        Debug.Log("jumpSpeedx: "+ jumpSpeedx);
        bool isFacingRight = transform.right.x < 0 ? false : true;
        Vector2 jumpDirection = isFacingRight ? new Vector2(1, 1).normalized : new Vector2(-1, 1).normalized;
        _rigidbody.AddForce(jumpDirection*jumpForce,ForceMode2D.Impulse);
        
        StartCoroutine(ReEnableColliderAfterDelay(collider, 0.1f));
    }
    
    IEnumerator ReEnableColliderAfterDelay(Collider2D collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
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

    public void GetHurt(Transform attack)
    {
        isHurt = true;
        _rigidbody.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attack.position.x), 0).normalized;
        _rigidbody.AddForce(dir*hurtForce,ForceMode2D.Impulse);
    }
    
    public void PlayerDead()
    {
        isDead = true;
    }
}
