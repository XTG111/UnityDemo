using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private PlayerPhyCheck _playerPhyCheck;
    private PawnMove _pawnMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerPhyCheck = GetComponent<PlayerPhyCheck>();
        _pawnMove = GetComponent<PawnMove>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        _animator.SetFloat("speed",Mathf.Abs(_rigidbody2D.velocity.x));
        _animator.SetFloat("velocityY",_rigidbody2D.velocity.y);
        _animator.SetBool("isGround",_playerPhyCheck.bIsGround);
        _animator.SetBool("isDead",_pawnMove.isDead);
    }

    public void PlayHurt()
    {
        _animator.SetTrigger("hurt");
    }
}
