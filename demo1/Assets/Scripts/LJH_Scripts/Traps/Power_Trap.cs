using System;
using System.Net.Http.Headers;
using UnityEngine;

public class Power_Trap : MonoBehaviour
{
    public float chargeTime = 2.0f;  // ����ʱ�䣨�룩
    public int damageAmount = 50;    // �߶��˺�

    private float timer = 0.0f;      // ������ʱ��
    private bool isCharging = false; // �Ƿ���������

    private Attack _attack;

    private void Awake()
    {
        _attack = GetComponent<Attack>();
    }

    private void Start()
    {
        // ����Ϸ��ʼʱ���������ʱ��ʼ����
        StartCharging();
    }

    private void Update()
    {
        if (isCharging)
        {
            timer += Time.deltaTime;

            // �������ʱ���ѹ������ͷ�����
            if (timer >= chargeTime)
            {
                ReleaseTrap();
            }
        }
    }

    private void StartCharging()
    {
        isCharging = true;
        timer = 0.0f;  // ���ü�ʱ��
    }

    private void ReleaseTrap()
    {
        isCharging = false;  // ֹͣ����

        // ��ȡ�����λ��
        float trapY = transform.position.y;

        // ���Ҳ��˺�ͬһ Y ���ϵĵ���
        Collider2D[] enemies = Physics2D.OverlapAreaAll(
            new Vector2(transform.position.x - 10f, trapY - 0.5f),
            new Vector2(transform.position.x + 10f, trapY + 0.5f)
        );
        // ������˽ű�ͨ��
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // ���������һ����Ϊ EnemyStatus �Ľű��������˺�
                PlayerInfo playerInfo = enemy.GetComponent<PlayerInfo>();
                if (playerInfo != null)
                {
                    _attack.attackdamage = (int)damageAmount;
                    playerInfo.TakeDamage(_attack);
                }
            }
        }

        // �����������
        Destroy(gameObject);
    }
}
