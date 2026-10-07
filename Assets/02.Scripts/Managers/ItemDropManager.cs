using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public static ItemDropManager instance;

    public GameObject[] itemPrefabs; // 인덱스 = itemID

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void DropItem(int itemID, Vector3 position)
    {
        if (itemID < 0 || itemID >= itemPrefabs.Length)
        {
            Debug.LogWarning($"존재하지 않는 아이템 ID: {itemID}");
            return;
        }

        GameObject dropped = Instantiate(itemPrefabs[itemID], position, Quaternion.identity);
        Debug.Log($"[드롭 성공] 아이템 ID: {itemID}, 이름: {dropped.name}, 위치: {position}");
    }
}
