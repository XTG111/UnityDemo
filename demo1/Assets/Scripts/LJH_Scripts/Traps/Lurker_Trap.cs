using UnityEngine;

public class Lurker_Trap : MonoBehaviour
{
    public int damageAmount = 10;      // ÿ���˺��Ĵ�С
    public float damageInterval = 1.0f; // �˺����ʱ�䣨�룩

    private float damageTimer = 0.0f;  // ��ʱ�������ڸ����˺����
    private bool isActive = true;      // �����Ƿ���Ȼ��Ծ

    private void Start()
    {
        // ������������������Ա���������Ӱ��
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;  // ��������
            rb.isKinematic = true; // ��ֹ�ܵ���������Ӱ��
        }
    }

    private void Update()
    {
        if (isActive)
        {
            damageTimer += Time.deltaTime;

            // �����ʱ�������˺����ʱ�䣬������˺�
            if (damageTimer >= damageInterval)
            {
                DealDamageToEnemies();
                damageTimer = 0.0f; // ���ü�ʱ��
            }
        }
    }

    private void DealDamageToEnemies()
    {
        // ����������Ӵ��ĵ��ˣ���ͨ��
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 0.5f); // 0.5f �Ǵ����뾶�����Ը�����Ҫ����

        /*foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // ���������һ����Ϊ EnemyStatus �Ľű��������˺�
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
        // �����˽��봥��������ʱ����������Ϊ��Ծ
        if (other.CompareTag("Enemy"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �������뿪����������ʱ������ѡ��������Ϊ����Ծ
        if (other.CompareTag("Enemy"))
        {
            isActive = false;
        }
    }
}
