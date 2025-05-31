using UnityEngine;
using System.Collections.Generic;

public class ItemButton : MonoBehaviour
{
    public Item itemToAdd;
    public GameObject objectPopUP;

    // static 변수로 획득 상태를 저장 (게임 실행 중 유지됨)
    private static HashSet<string> collectedItems = new HashSet<string>();

    void Start()
    {
        // 이미 획득한 아이템이면 오브젝트 파괴
        if (collectedItems.Contains(itemToAdd.itemName))
        {
            Destroy(gameObject);
        }
    }

    public void OnClickItem()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddItemToInventory(itemToAdd);
        }

        // 획득 처리
        collectedItems.Add(itemToAdd.itemName);

        closeObjectPopUP();
        Destroy(gameObject);
    }

    public void openObjectPopUP()
    {
        objectPopUP.SetActive(true);
    }

    public void closeObjectPopUP()
    {
        objectPopUP.SetActive(false);
    }
}
