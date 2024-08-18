using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapSpawner : MonoBehaviour
{
    //���������������ص�GameManager���棬�轫�����Ԥ������ص��ýű���

    public Tilemap tilemap; 
    public GameObject[] trapPrefabs;  // ���ڴ洢��ͬ������Ԥ�Ƽ�
    private GameObject currentTrapPrefab;  // ��ǰѡ�������Ԥ�Ƽ�

    void Update()
    {
        // ������ּ�ѡ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTrapPrefab = trapPrefabs[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTrapPrefab = trapPrefabs[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentTrapPrefab = trapPrefabs[2];
        }

        // �����������������
        if (Input.GetMouseButtonDown(0) && currentTrapPrefab != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;  

            Instantiate(currentTrapPrefab, mousePosition, Quaternion.identity);
        }
    }
}
