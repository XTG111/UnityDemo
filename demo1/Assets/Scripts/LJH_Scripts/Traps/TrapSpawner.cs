using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TrapSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject[] trapPrefabs;  // 用于存储不同的陷阱预制件
    private GameObject currentTrapPrefab;  // 当前选择的陷阱预制件

    private HashSet<Vector3Int> occupiedPositions = new HashSet<Vector3Int>();  // 记录已放置陷阱的位置

    void Update()
    {
        // 检测数字键选择陷阱
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

        // 检测鼠标左键放置陷阱
        if (Input.GetMouseButtonDown(0) && currentTrapPrefab != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // 获取点击位置的Tile坐标
            Vector3Int tilePosition = tilemap.WorldToCell(mousePosition);

            // 确保Tilemap中的该位置有Tile且没有陷阱
            if (tilemap.GetTile(tilePosition) == null && !occupiedPositions.Contains(tilePosition))
            {
                // 获取Tile在Tilemap中的位置
                Vector3 trapPosition = tilemap.GetCellCenterWorld(tilePosition);

                // 实例化陷阱
                Instantiate(currentTrapPrefab, trapPosition, Quaternion.identity);

                // 记录该位置已被占用
                occupiedPositions.Add(tilePosition);
            }
        }
    }
}
