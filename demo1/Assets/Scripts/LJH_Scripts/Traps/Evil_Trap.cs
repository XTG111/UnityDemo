using UnityEngine;
using System.Collections;

public class Evil_Trap : MonoBehaviour
{
    // ������ʧ������˵��˺����ӵı���
    public float damageMultiplier = 2f;

    // ������ʧ�� debuff ����ʱ��
    public float debuffDuration = 5f;

    // ��������Χ
    public float triggerRadius = 1f;

    private void Start()
    {
        // Ϊ������� Rigidbody2D �����ʵ������
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;  // �����ʵ�������ֵ
        rb.isKinematic = true;  // ��ֹ Rigidbody2D �ܵ�������Ӱ��
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��鴥���Ķ����Ƿ��ǵ���
        //if (other.CompareTag("Enemy"))
        {
            // ��ȡ���˶����״̬��������������(������ͨ��)
            //EnemyStatus enemyStatus = other.GetComponent<EnemyStatus>();

            //if (enemyStatus != null)
            //{
                // ������� debuff
            //    StartCoroutine(ApplyDebuff(enemyStatus));
            //}

            // �����������
            //Destroy(gameObject);
        }
    }

    /*private IEnumerator ApplyDebuff(EnemyStatus enemyStatus)
    {
        // ��¼ԭʼ�˺�����
        float originalDamageMultiplier = enemyStatus.damageMultiplier;

        // Ӧ�� debuff
        enemyStatus.damageMultiplier = damageMultiplier;

        // �ȴ� debuff ����ʱ��
        yield return new WaitForSeconds(debuffDuration);

        // �ָ�ԭʼ�˺�����
        enemyStatus.damageMultiplier = originalDamageMultiplier;
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
