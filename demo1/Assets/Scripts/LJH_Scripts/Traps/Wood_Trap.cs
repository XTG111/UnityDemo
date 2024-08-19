using UnityEngine;

public class Wood_Trap : MonoBehaviour
{
    public float lifetime = 10f; // ���������ʱ��

    private void Start()
    {
        // ȷ��ľ������������
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // ��������
            rb.isKinematic = true; // ȷ��ľ�����岻�������������ƶ�
        }

        // ������ʱ���Ա���ָ��ʱ�����������
        Invoke(nameof(DestroyTrap), lifetime);
    }

    private void DestroyTrap()
    {
        Destroy(gameObject); // ����������Ϸ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����������ײ���߼�
        // ���������Ӵ������赲���˵��ƶ���
    }
}
