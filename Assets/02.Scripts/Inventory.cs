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
        for (int i = 0; i < slots.Length; i++)
        {
            if (i >= items.Count)
            {
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

        Debug.Log("슬롯이 가득 차 있습니다.");
    }

    /// <summary>
    /// 지정된 이름의 아이템이 인벤토리에 있는지 확인합니다.
    /// </summary>
    public bool HasItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item != null && item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemByName(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null && items[i].itemName == itemName)
            {
                items[i] = null;
                FreshSlot(); // UI 갱신
                return;
            }
        }

        Debug.Log("해당 이름의 아이템이 인벤토리에 없습니다.");
    }



}
