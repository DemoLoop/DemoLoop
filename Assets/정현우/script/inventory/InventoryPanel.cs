// 2024-09-12, 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.Instance.dragAndDropController.OnClick(inventory.slots[id]);
        Show();
    }
}
