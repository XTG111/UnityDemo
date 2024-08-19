using UnityEngine;
using System.Collections;

public class Evil_Trap : MonoBehaviour
{
    // 陷阱消失后给敌人的伤害增加的倍数
    public float damageMultiplier = 2f;

    // 陷阱消失后 debuff 持续时间
    public float debuffDuration = 5f;

    // 触发器范围
    public float triggerRadius = 1f;

    private void Start()
    {
        // 为陷阱添加 Rigidbody2D 组件以实现重力
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;  // 设置适当的重力值
        rb.isKinematic = true;  // 防止 Rigidbody2D 受到物理力影响
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发的对象是否是敌人
        //if (other.CompareTag("Enemy"))
        {
            // 获取敌人对象的状态管理器或相关组件(需完善通信)
            //EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();

            //if (enemyStatus != null)
            //{
                // 给予敌人 debuff
            //    StartCoroutine(ApplyDebuff(enemyStatus));
            //}

            // 销毁陷阱对象
            //Destroy(gameObject);
        }
    }

    /*private IEnumerator ApplyDebuff(EnemyStatus enemyStatus)
    {
        // 记录原始伤害倍率
        float originalDamageMultiplier = enemyStatus.damageMultiplier;

        // 应用 debuff
        enemyStatus.damageMultiplier = damageMultiplier;

        // 等待 debuff 持续时间
        yield return new WaitForSeconds(debuffDuration);

        // 恢复原始伤害倍率
        enemyStatus.damageMultiplier = originalDamageMultiplier;
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
