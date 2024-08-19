using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPhyCheck : MonoBehaviour
{
    private CapsuleCollider2D _capCollider2D;//需要更改
    private IInteractable _targetItem;
    private PawnMove _pawnMove;
    private PlayerBeheviour _playerBeheviour;
    
    public bool manual;
    
    public float checkRaius;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    [Header("当前状态")] 
    public bool bIsGround;
    public bool isTouchLeftWall;
    public bool isTouchRightWall;

    [Header("检测墙")] 
    public float lowY = 0.5f;
    public float highY = 1.5f;
    public float raydisX = 1.1f;
    public LayerMask groundLayer;

    [Header("检测坑")] 
    public float nearX = 0.5f;
    public float farX = 2.5f;
    public float raydisY = 2.0f;

    [Header("当前离地高度")] 
    public float damageHigh = 4.0f;
    private float curHigh = 0;
    [Header("下落伤害")]
    public float baseDamage = 5.0f;
    public float addDamage = 1.0f;
    
    public bool canJump;
    
    [Header("恶魔之泪状态")]
    public bool underEmo = false;
    public float debuffTime = 0.0f;

    private void Awake()
    {
        _capCollider2D = GetComponent<CapsuleCollider2D>();
        _pawnMove = GetComponent<PawnMove>();
        _playerBeheviour = GetComponent<PlayerBeheviour>();
        if (!manual)
        {
            rightOffset = new Vector2(
                (_capCollider2D.bounds.size.x + _capCollider2D.offset.x) / 2,
                _capCollider2D.size.y / 2
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
        CheckWall();
        CheckKeng();
        if(!bIsGround) CheckHigh();
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
    
    //检测墙的高度和坑的宽度
    private void CheckWall()
    {
        //检测正前方墙壁
        bool isFacingRight = transform.localScale.x > 0;
        // 射线的方向根据角色朝向调整
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        Vector2 lowpoint = new Vector2(transform.position.x, transform.position.y + lowY);
        RaycastHit2D hitx = Physics2D.Raycast(lowpoint, direction, raydisX, groundLayer);
        //
        Vector2 heighpoint = new Vector2(transform.position.x, transform.position.y + highY);
        RaycastHit2D hity = Physics2D.Raycast(heighpoint, direction, raydisX, groundLayer);
        if (bIsGround && hitx.collider != null && hity.collider == null)
        {
            _pawnMove.Jump();
        }

        if (bIsGround && hitx.collider != null && hity.collider != null)
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void CheckKeng()
    {
    //检测坑
        bool isFacingRight = transform.localScale.x > 0;
        var checkpoint1 = new Vector2(transform.position.x + (isFacingRight ? nearX : -nearX), transform.position.y+0.5f);
        RaycastHit2D hitk1 = Physics2D.Raycast(checkpoint1, Vector2.down, 2f, groundLayer);
        var checkpoint2 = new Vector2(transform.position.x + (isFacingRight ? farX : -farX), transform.position.y+0.5f);
        RaycastHit2D hitk2 = Physics2D.Raycast(checkpoint2, Vector2.down, raydisY, groundLayer);

        if (bIsGround && hitk1.collider == null && hitk2.collider != null)
        {
            _pawnMove.Jump();
        }

        if (bIsGround && hitk1.collider == null && hitk2.collider == null)
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void CheckHigh()
    {
        bool isFacingRight = transform.localScale.x > 0;
        // 射线的方向根据角色朝向调整
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        Vector2 lowpoint = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hitk1 = Physics2D.Raycast(lowpoint, Vector2.down, 50f, groundLayer);
        if (hitk1.collider != null)
        {
            var high = Vector2.Distance(transform.position, hitk1.point);
            curHigh = Mathf.Max(high, curHigh);
        }
        else curHigh = 50.0f;
    }
    
    public void HighDamage()
    {
        if (bIsGround && curHigh >= damageHigh)
        {
            if (_playerBeheviour.curState == BehaviourState.BoomPlayer && _playerBeheviour.bst != BoomState.Seco_TakeDamage)
            {
                baseDamage *= addDamage;
            }
            else baseDamage = 1.0f;
            int damage = (int)(baseDamage * curHigh);
            int range = 1;
            Attack highAttack = new Attack(damage,range);
            gameObject.GetComponent<PlayerInfo>()?.TakeDamage(highAttack);
        }

        curHigh = 0.0f;
    }
    
    //检测Buff
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            _targetItem = other.GetComponent<IInteractable>();
            //_targetItem.TriggerAction(gameObject);
        }
        
        //TODO: 恶魔之泪
        if (other.CompareTag(""))
        {
            if (underEmo == false)
            {
                //读取debuff时间
                underEmo = true;
                //debuffTime = 
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        var localScale = transform.localScale;
        Gizmos.DrawWireSphere((Vector2)pos + bottomOffset*localScale, checkRaius);
        Gizmos.DrawWireSphere((Vector2)pos + leftOffset, checkRaius);
        Gizmos.DrawWireSphere((Vector2)pos + rightOffset, checkRaius);
        
        var checkpoint1 = new Vector2(transform.position.x - 0.5f, transform.position.y+0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(checkpoint1, checkpoint1 + Vector2.down * 2);
        var checkpoint2 = new Vector2(transform.position.x - 2.5f, transform.position.y+0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkpoint2, checkpoint2 + Vector2.down * 2);
        
    }
}
