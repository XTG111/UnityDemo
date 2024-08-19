using UnityEngine;

public class Wood_Trap : MonoBehaviour
{
    public float lifetime = 10f; // 陷阱的生存时间

    private void Start()
    {
        // 确保木块陷阱无重力
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // 禁用重力
            rb.isKinematic = true; // 确保木块陷阱不会因物理交互而移动
        }

        // 启动定时器以便在指定时间后销毁陷阱
        Invoke(nameof(DestroyTrap), lifetime);
    }

    private void DestroyTrap()
    {
        Destroy(gameObject); // 销毁陷阱游戏对象
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 处理与敌人碰撞的逻辑
        // 这里可以添加代码以阻挡敌人的移动等
    }
}
