using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb_Trap : MonoBehaviour
{
    public float fallSpeed = 5f; // 重力加速度
    public float explosionDelay = 1f; // 爆炸延迟
    public float explosionRadius = 3f; // 爆炸半径
    public float damage = 50f; // 爆炸伤害
    public LayerMask enemyLayer; // 敌人的层级

    private Attack _attack;
    private Tilemap bridgeTilemap1;
    private Tilemap bridgeTilemap2;
    private Tilemap bridgeTilemap3;
    private Tilemap bridgeTilemap4;
    private Tilemap platformTilemap;

    private bool hasExploded = false;

    [Header("炸弹前的位置")] 
    public Vector3 LastLoc;
    public bool isBoom;

    private void Awake()
    {
        _attack = GetComponent<Attack>();
    }

    private void Start()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = fallSpeed;
        rb.bodyType = RigidbodyType2D.Dynamic;

        bridgeTilemap1 = GameObject.Find("Bridge1")?.GetComponent<Tilemap>();
        bridgeTilemap2 = GameObject.Find("Bridge2")?.GetComponent<Tilemap>();
        bridgeTilemap3 = GameObject.Find("Bridge3")?.GetComponent<Tilemap>();
        bridgeTilemap4 = GameObject.Find("Bridge4")?.GetComponent<Tilemap>();
        platformTilemap = GameObject.Find("Platform")?.GetComponent<Tilemap>();

        if (bridgeTilemap1 == null)
        {
            Debug.LogWarning("Bridge Tilemap not found!");
        }
        if (platformTilemap == null)
        {
            Debug.LogWarning("Platform Tilemap not found!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasExploded)
        {
            hasExploded = true;
            Invoke("Explode", explosionDelay);
        }
    }

    private void Explode()
    {
        // 伤害敌人
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            PlayerInfo playerInfo = enemy.GetComponent<PlayerInfo>();
            bool NoDamage = true;
            if(playerInfo) 
            {
                NoDamage = playerInfo.GetComponent<PlayerBeheviour>().bst == BoomState.Init_NoDamage &&
                             playerInfo.GetComponent<PlayerBeheviour>().curState == BehaviourState.BoomPlayer;
            }
            if (playerInfo != null && !NoDamage)
            {
                // 调用 TakeDamage 函数
                _attack.lastLoc = playerInfo.transform.position;
                _attack.isBoom = true;
                _attack.attackdamage = (int)damage;
                _attack.attackRange = explosionRadius;
                playerInfo.TakeDamage(_attack);
            }
        }

        // 确保 Tilemap 组件存在后摧毁瓦片
        if (bridgeTilemap1 != null)
        {
            DestroyTilesInTilemap(bridgeTilemap1);
        }
        if (bridgeTilemap2 != null)
        {
            DestroyTilesInTilemap(bridgeTilemap2);
        }
        if (bridgeTilemap3 != null)
        {
            DestroyTilesInTilemap(bridgeTilemap3);
        }
        if (bridgeTilemap4 != null)
        {
            DestroyTilesInTilemap(bridgeTilemap4);
        }
        if (platformTilemap != null)
        {
            DestroyTilesInTilemap(platformTilemap);
        }

        // 销毁炸弹自身
        Destroy(gameObject);
    }

    private void DestroyTilesInTilemap(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(cellPosition) != null)
                {
                    float distance = Vector3.Distance(transform.position, tilemap.GetCellCenterWorld(cellPosition));
                    if (distance <= explosionRadius)
                    {
                        tilemap.SetTile(cellPosition, null);
                    }
                }
            }
        }
    }
}
