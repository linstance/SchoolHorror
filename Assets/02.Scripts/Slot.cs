using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;

    private Item _item;
    public Item item {
        get { return _item; }
        set {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);  // 아이템이 있을 때 이미지 보이기
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);  // 아이템이 없을 때 이미지 숨기기
            }
        }
    }
}