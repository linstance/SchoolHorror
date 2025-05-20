using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public Item itemToAdd;  // 이 버튼에 연결된 아이템

    public void OnClickItem()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddItemToInventory(itemToAdd);  // 버튼에 연결된 아이템을 인벤토리에 추가
        }
    }
}
