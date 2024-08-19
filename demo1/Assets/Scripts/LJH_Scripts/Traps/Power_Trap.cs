using System;
using System.Net.Http.Headers;
using UnityEngine;

public class Power_Trap : MonoBehaviour
{
    public float chargeTime = 2.0f;  // 蓄力时间（秒）
    public int damageAmount = 50;    // 高额伤害

    private float timer = 0.0f;      // 蓄力计时器
    private bool isCharging = false; // 是否正在蓄力

    private Attack _attack;

    private void Awake()
    {
        _attack = GetComponent<Attack>();
    }

    private void Start()
    {
        // 在游戏开始时或陷阱放置时开始蓄力
        StartCharging();
    }

    private void Update()
    {
        if (isCharging)
        {
            timer += Time.deltaTime;

            // 如果蓄力时间已过，则释放陷阱
            if (timer >= chargeTime)
            {
                ReleaseTrap();
            }
        }
    }

    private void StartCharging()
    {
        isCharging = true;
        timer = 0.0f;  // 重置计时器
    }

    private void ReleaseTrap()
    {
        isCharging = false;  // 停止蓄力

        // 获取陷阱的位置
        float trapY = transform.position.y;

        // 查找并伤害同一 Y 轴上的敌人
        Collider2D[] enemies = Physics2D.OverlapAreaAll(
            new Vector2(transform.position.x - 10f, trapY - 0.5f),
            new Vector2(transform.position.x + 10f, trapY + 0.5f)
        );
        // 需与敌人脚本通信
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // 假设敌人有一个名为 EnemyStatus 的脚本来处理伤害
                PlayerInfo playerInfo = enemy.GetComponent<PlayerInfo>();
                if (playerInfo != null)
                {
                    _attack.attackdamage = (int)damageAmount;
                    playerInfo.TakeDamage(_attack);
                }
            }
        }

        // 销毁陷阱对象
        Destroy(gameObject);
    }
}
