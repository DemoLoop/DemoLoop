// 2024-09-12 작성자 : 정현우

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] //클래스나 구조체 자체를 직렬화 가능, [SerializeField] 변수를 직렬화
public class ItemSlot
{
    public item item; // 이 슬롯에 들어있는 아이템
    public int count; // 인벤토리 칸에 있는 아이템의 개수

    // 다른 ItemSlot의 정보를 복사하는 메서드
    public void Copy(ItemSlot slot)
    {
        item = slot.item; // 아이템 복사
        count = slot.count; // 개수 복사
    }

    // 아이템과 개수를 설정하는 메서드
    public void Set(item item, int count)
    {
        this.item = item; // 슬롯에 아이템 설정
        this.count = count; // 슬롯에 아이템 개수 설정
    }

    // 슬롯을 비우는 메서드
    public void Clear()
    {
        item = null; // 아이템을 null로 설정 (비움)
        count = 0; // 개수를 0으로 설정
    }
}

// ItemContainer 클래스는 여러 개의 ItemSlot을 관리하는 클래스입니다.
[CreateAssetMenu(menuName = "Data/Item Container")] 
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots; // 여러 개의 ItemSlot을 저장하는 리스트
    public bool isDirty;

    // 아이템을 인벤토리에 추가하는 메서드
    public void Add(item item, int count = 1)
    {
        isDirty = true;

        // 쌓을 수 있는 아이템일 경우
        if (item.stackable == true)
        {
            // 이미 같은 아이템이 있는 슬롯을 찾습니다.
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                // 같은 아이템이 있으면 개수를 증가시킵니다.
                itemSlot.count += count;
            }
            else
            {
                // 빈 슬롯을 찾아서 아이템을 추가합니다.
                itemSlot = slots.Find(x => x.item == null);

                if (itemSlot != null)
                {
                    itemSlot.item = item; // 아이템 설정
                    itemSlot.count = count; // 개수 설정
                }
            }
        }
        else
        {
            // 쌓을 수 없는 아이템일 경우
            ItemSlot itemSlot = slots.Find(x => x.item == null);

            if (itemSlot != null)
            {
                itemSlot.item = item; // 빈 슬롯에 아이템 설정
            }
        }
    }

    // 아이템을 인벤토리에서 제거하는 메서드
    public void Remove(item itemToRemove, int count = 1)
    {
        isDirty = true;

        if (itemToRemove.stackable)
        {
            // 쌓을 수 있는 아이템일 경우
            ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);

            if (itemSlot == null) { return; } // 아이템이 없으면 종료

            itemSlot.count -= count; // 개수 감소

            if (itemSlot.count <= 0)
            {
                itemSlot.Clear(); // 개수가 0 이하가 되면 슬롯 비우기
            }
        }
        else
        {
            // 쌓을 수 없는 아이템일 경우
            while (count > 0)
            {
                count -= 1; // 제거할 개수 감소

                ItemSlot itemSlot = slots.Find(x => x.item == itemToRemove);

                if (itemSlot == null) { break; } // 아이템이 없으면 종료
                itemSlot.Clear(); // 슬롯 비우기
            }
        }
    }

    internal bool CheckFreeSpace()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return true;
            }
        }

        return false;
    }

    internal bool CheckItem(ItemSlot checkingItem) // 아이템에 필요한 장비 채크
    {
        ItemSlot itemSlot = slots.Find(x => x.item == checkingItem.item);

        if (itemSlot == null) { return false; }

        if (checkingItem.item.stackable) { return itemSlot.count >= checkingItem.count; }

        return true;
    }
}



