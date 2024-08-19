using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TrapSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject[] trapPrefabs;  // ���ڴ洢��ͬ������Ԥ�Ƽ�
    private GameObject currentTrapPrefab;  // ��ǰѡ�������Ԥ�Ƽ�

    private HashSet<Vector3Int> occupiedPositions = new HashSet<Vector3Int>();  // ��¼�ѷ��������λ��

    public int maxTrapCount = 10;  // ������ɴ���
    private int currentTrapCount = 0;  // ��ǰ�����ɵ���������

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
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentTrapPrefab = trapPrefabs[3];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentTrapPrefab = trapPrefabs[4];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentTrapPrefab = trapPrefabs[5];
        }

        // �����������������
        if (Input.GetMouseButtonDown(0) && currentTrapPrefab != null)
        {
            // ȷ����ǰ���ɵ���������δ����������ɴ���
            if (currentTrapCount >= maxTrapCount)
            {
                Debug.Log("�Ѵﵽ����������ɴ�����");
                return;
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // ��ȡ���λ�õ�Tile����
            Vector3Int tilePosition = tilemap.WorldToCell(mousePosition);

            // ȷ��Tilemap�еĸ�λ����Tile��û������
            if (tilemap.GetTile(tilePosition) == null && !occupiedPositions.Contains(tilePosition))
            {
                // ��ȡTile��Tilemap�е�λ��
                Vector3 trapPosition = tilemap.GetCellCenterWorld(tilePosition);

                // ʵ��������
                Instantiate(currentTrapPrefab, trapPosition, Quaternion.identity);

                // ��¼��λ���ѱ�ռ��
                occupiedPositions.Add(tilePosition);

                // ���������ɵ���������
                currentTrapCount++;
            }
        }
    }
}
