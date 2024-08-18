using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapSpawner : MonoBehaviour
{
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

            // ��ȡ���λ�õ�Tile����
            Vector3Int tilePosition = tilemap.WorldToCell(mousePosition);

            // ȷ��Tilemap�еĸ�λ����Tile
            if (tilemap.GetTile(tilePosition) != null)
            {
                // ��ȡTile��Tilemap�е�λ��
                Vector3 trapPosition = tilemap.GetCellCenterWorld(tilePosition);

                // ʵ��������
                Instantiate(currentTrapPrefab, trapPosition, Quaternion.identity);
            }
        }
    }
}
