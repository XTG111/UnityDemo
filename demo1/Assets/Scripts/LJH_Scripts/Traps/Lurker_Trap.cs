using UnityEngine;

public class Lurker_Trap : MonoBehaviour
{
    public int damageAmount = 10;      // 每次伤害的大小
    public float damageInterval = 1.0f; // 伤害间隔时间（秒）

    private float damageTimer = 0.0f;  // 计时器，用于跟踪伤害间隔
    private bool isActive = true;      // 陷阱是否仍然活跃

    private void Start()
    {
        // 设置陷阱的物理属性以避免受重力影响
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;  // 禁用重力
            rb.isKinematic = true; // 防止受到物理力的影响
        }
    }

    private void Update()
    {
        if (isActive)
        {
            damageTimer += Time.deltaTime;

            // 如果计时器超过伤害间隔时间，则造成伤害
            if (damageTimer >= damageInterval)
            {
                DealDamageToEnemies();
                damageTimer = 0.0f; // 重置计时器
            }
        }
    }

    private void DealDamageToEnemies()
    {
        // 查找与陷阱接触的敌人，需通信
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 0.5f); // 0.5f 是触发半径，可以根据需要调整

        /*foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // 假设敌人有一个名为 EnemyStatus 的脚本来处理伤害
                EnemyStatus enemyStatus = enemy.GetComponent<EnemyStatus>();
                if (enemyStatus != null)
                {
                    enemyStatus.TakeDamage(damageAmount);
                }
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 当敌人进入触发器区域时，将陷阱设为活跃
        if (other.CompareTag("Enemy"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 当敌人离开触发器区域时，可以选择将陷阱设为不活跃
        if (other.CompareTag("Enemy"))
        {
            isActive = false;
        }
    }
}
