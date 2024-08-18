using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhyCheck : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;//需要更改
    private IInteractable _targetItem;
    
    public LayerMask groundLayer;
    
    public bool manual;
    
    public float checkRaius;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    [Header("当前状态")] 
    public bool bIsGround;
    public bool isTouchLeftWall;
    public bool isTouchRightWall;
    
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2(
                (_boxCollider2D.bounds.size.x + _boxCollider2D.offset.x) / 2,
                _boxCollider2D.size.y / 2
            );
            leftOffset = new Vector2(
                -rightOffset.x,
                rightOffset.y
            );
        }
    }


    private void Update()
    {
        Check();
    }

    public void Check()
    {
        Vector3 pos = transform.position;
        //检测地面
        bIsGround =Physics2D.OverlapCircle((Vector2)pos + bottomOffset*transform.localScale, checkRaius, groundLayer);
        //墙体
        isTouchLeftWall = Physics2D.OverlapCircle((Vector2)pos + leftOffset, checkRaius, groundLayer);
        isTouchRightWall = Physics2D.OverlapCircle((Vector2)pos + rightOffset, checkRaius, groundLayer);
    }
    
    //检测Buff
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            _targetItem = other.GetComponent<IInteractable>();
            _targetItem.TriggerAction(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        Gizmos.DrawWireSphere((Vector2)pos + bottomOffset*transform.localScale, checkRaius);
        Gizmos.DrawWireSphere((Vector2)pos + leftOffset, checkRaius);
        Gizmos.DrawWireSphere((Vector2)pos + rightOffset, checkRaius);
    }
}
