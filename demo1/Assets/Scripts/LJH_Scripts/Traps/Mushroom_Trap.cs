using UnityEngine;

public class Mushroom_Trap : MonoBehaviour
{
    // �����Ĵ�С
    public float bounceForce = 10f;

    // Ģ������Ĵ�������Χ
    public float triggerRadius = 1f;

    // ��ײ�����
    private void Start()
    {
        // ȷ���������һ�� Collider2D ���
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("Mushroom_Trap requires a Collider2D component.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��鴥���Ķ����Ƿ��ǵ���
        if (other.CompareTag("Enemy"))
        {
            // ��ȡ���˵� Rigidbody2D ���
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();

            if (enemyRigidbody != null)
            {
                // �������һ�����ϵ���
                enemyRigidbody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
