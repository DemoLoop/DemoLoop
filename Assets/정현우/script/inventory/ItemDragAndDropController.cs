// 2024-09-19 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 아이템을 드래그 앤 드롭하는 기능을 처리하는 클래스
public class ItemDragAndDropController : MonoBehaviour
{
    // 드래그하는 아이템 슬롯
    [SerializeField] public ItemSlot itemSlot;

    // 아이템 아이콘을 나타내는 게임 오브젝트
    [SerializeField] GameObject itemIcon;

    // UI가 그려지는 캔버스
    [SerializeField] Canvas canvas;

    // 아이콘의 위치와 크기를 조정하는 RectTransform
    RectTransform iconTransform;

    // 아이콘의 이미지를 나타내는 Image 컴포넌트
    Image itemIconImage;

    // 처음에 실행되는 함수 (초기화)
    private void Start()
    {
        // 새로운 아이템 슬롯을 생성 (GameManager에서 설정하는 것이 바람직함)
        itemSlot = new ItemSlot();

        // 아이콘의 RectTransform을 가져옴 (크기와 위치를 조정하기 위해 필요)
        iconTransform = itemIcon.GetComponent<RectTransform>();

        // 아이콘의 이미지를 가져옴 (아이템에 따라 다른 이미지로 변경)
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    public bool CheckForSale()
    {
        if (itemSlot.item == null) { return false; }
        if (itemSlot.item.canBeSold == false) { return false; }

        return true;
    }

    // 매 프레임마다 실행되는 함수
    private void Update()
    {
        // 아이템 아이콘이 활성화되어 있을 때만 동작
        if (itemIcon.activeInHierarchy == true)
        {
            // 마우스 좌표를 캔버스 좌표로 변환 (아이콘이 마우스를 따라다니도록)
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                canvas.GetComponent<RectTransform>(),  // 캔버스의 RectTransform
                Input.mousePosition,                   // 마우스의 스크린 좌표
                canvas.worldCamera,                    // 캔버스에 할당된 카메라
                out localPoint                         // 변환된 로컬 좌표
            );

            // 변환된 좌표를 아이콘의 위치로 설정
            iconTransform.localPosition = localPoint;

            // 마우스 왼쪽 버튼이 눌렸을 때
            if (Input.GetMouseButton(0))
            {
                // 인벤토리 안에서 클릭한 것인지 확인
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    // 인벤토리 외부를 클릭하면 아이템을 스폰
                    Vector3 worldPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPostion.z = 0;

                    // 아이템을 현재 마우스 위치에 스폰
                    itemSpawnManager.instance.SpawnItem(worldPostion, itemSlot.item, itemSlot.count);

                    // 아이템 슬롯을 비우고 아이콘을 비활성화
                    itemSlot.Clear();
                    itemIcon.SetActive(false);
                }
            }
        }
    }

    // 아이템 슬롯을 클릭했을 때 호출되는 함수
    internal void OnClick(ItemSlot itemSlot)
    {
        // 현재 아이템 슬롯에 아이템이 없으면, 선택한 아이템 슬롯의 정보를 복사
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();  // 선택한 슬롯은 비워줌
        }
        else // 현재 슬롯에 아이템이 있으면, 두 슬롯의 아이템을 교체
        {
            item item = itemSlot.item;
            int count = itemSlot.count;

            // 현재 슬롯의 아이템 정보를 복사
            itemSlot.Copy(this.itemSlot);
            // 선택한 슬롯에 원래 있던 아이템을 다시 설정
            this.itemSlot.Set(item, count);
        }
        // 아이콘을 업데이트
        UpdateIcon();
    }

    // 아이템 아이콘을 업데이트하는 함수
    public void UpdateIcon()
    {
        // 슬롯에 아이템이 없으면 아이콘을 숨김
        if (itemSlot.item == null)
        {
            itemIcon.SetActive(false);
        }
        else // 슬롯에 아이템이 있으면 아이콘을 표시하고 이미지 변경
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;  // 아이템에 맞는 이미지로 변경
        }
    }
}