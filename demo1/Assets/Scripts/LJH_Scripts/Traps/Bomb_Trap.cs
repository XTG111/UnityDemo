using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb_Trap : MonoBehaviour
{
    public float fallSpeed = 5f; // �������ٶ�
    public float explosionDelay = 1f; // ��ը�ӳ�
    public float explosionRadius = 3f; // ��ը�뾶
    public float damage = 50f; // ��ը�˺�
    public LayerMask enemyLayer; // ���˵Ĳ㼶

    private Tilemap bridgeTilemap1;
    private Tilemap bridgeTilemap2;
    private Tilemap bridgeTilemap3;
    private Tilemap bridgeTilemap4;
    private Tilemap platformTilemap;

    private bool hasExploded = false;

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
        // �˺�����
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        Attack attack = new Attack();
        foreach (Collider2D enemy in enemies)
        {
            PlayerInfo playerInfo = enemy.GetComponent<PlayerInfo>();
            if (playerInfo != null)
            {
                // ���� TakeDamage ����
                playerInfo.TakeDamage(attack);
            }
        }

        // ȷ�� Tilemap ������ں�ݻ���Ƭ
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

        // ����ը������
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
