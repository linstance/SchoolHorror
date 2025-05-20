using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    [SerializeField] private Transform slotParent;
    [SerializeField] private Slot[] slots;

    void Awake()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item newItem)
    {
        // 아이템 리스트 내에서 비어있는 인덱스를 찾기
        for (int i = 0; i < slots.Length; i++)
        {
            if (i >= items.Count)
            {
                // 리스트에 공간이 없는 경우 추가
                items.Add(newItem);
                FreshSlot();
                return;
            }

            if (items[i] == null)
            {
                items[i] = newItem;
                FreshSlot();
                return;
            }
        }

        // 여기에 도달했다면 빈 슬롯이 없음
        Debug.Log("슬롯이 가득 차 있습니다.");
    }

}