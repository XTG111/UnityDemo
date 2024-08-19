using UnityEngine;

public class Mushroom_Trap : MonoBehaviour
{
    // 弹力的大小
    public float bounceForce = 10f;

    // 蘑菇陷阱的触发器范围
    public float triggerRadius = 1f;

    // 销毁延迟时间
    public float destroyDelay = 10f;

    private void Start()
    {
        // 确保陷阱具有一个 Collider2D 组件
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("Mushroom_Trap requires a Collider2D component.");
        }

        // 设置销毁计时器
        Invoke("DestroyTrap", destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发的对象是否是敌人
        if (other.CompareTag("Enemy"))
        {
            // 获取敌人的 Rigidbody2D 组件
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();

            if (enemyRigidbody != null)
            {
                // 给予敌人一个向上的力
                enemyRigidbody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }

    // 自动销毁陷阱的方法
    private void DestroyTrap()
    {
        Destroy(gameObject);
    }
}
