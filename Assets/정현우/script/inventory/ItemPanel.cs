// 2024-09-19 작성자 : 정현우
// 2024-12-14 스크립트 일부 수정, 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetSourcePanel(); // 12-14
        SetIndex();
        Show();
    }

    // 12-14
    private void SetSourcePanel()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetItemPanel(this);
        }
    }

    private void OnEnable()
    {
        Clear(); //12-14
        Show();
    }

    private void LateUpdate()
    {
        if (inventory == null) { return; }

        if (inventory.isDirty)
        {
            Show();
            inventory.isDirty = false;
        }
    }

    private void SetIndex()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public virtual void Show()
    {
        if (inventory == null) { return; }

        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }

    // 초기화를 하지 않으면, 상점마다 각자 다른 물건을 팔 수가 없음 
    public void Clear()
    { 
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Clean();
        }
    }


    public void SetInventory(ItemContainer newInventory)
    {
        inventory = newInventory;
    }


    public virtual void OnClick(int id)
    { 
        
    }
}
