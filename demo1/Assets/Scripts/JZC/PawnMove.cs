using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//校验功能的基础移动
public class PawnMove : MonoBehaviour
{
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
        if(_ppcheck.bIsGround) Move();
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
}
