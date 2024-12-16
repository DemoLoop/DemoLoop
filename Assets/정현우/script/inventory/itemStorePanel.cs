// 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemStorePanel : ItemPanel
{
    [SerializeField] Trading trading;
    public override void OnClick(int id)
    {
        if (GameManager.Instance.dragAndDropController.itemSlot.item == null) // 수정
        {
            BuyItem(id);
        }
        else
        {
            Selltem();
        }

        Show();
    }

    public void BuyItem(int id)
    {
        trading.BuyItem(id);
    }

    public void Selltem()
    {
        trading.SellItem();
    }

}
   

