//2024-09-12 작성자 : 정현우
//2024-12-14 수정 : 정현우

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryButton : MonoBehaviour, IPointerClickHandler
    // ui를 클릭할 때마다 실행하는 인터페이스
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;

    int myIndex;

    ItemPanel itemPanel;

    public void SetIndex(int index)
    { 
        myIndex = index;
    }

    public void SetItemPanel(ItemPanel source) 
    { 
        itemPanel = source;
    }

    public void Set(ItemSlot slot) //화면에서 표시할 항목
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;

        if(slot.item.stackable == true)  //여러개 소유할 수 있는 아이템은 숫자표시
        {
            text.gameObject.SetActive(true);
            text.text = slot.count.ToString();
        }
        else //여러개 소유할 수 없는 아이템은 숫자표시 하지 않음
        {
            text.gameObject.SetActive(false);
        }
    }

    public void Clean() //아이콘의 스프라이트, 텍스트 제거
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    
    // 이 버튼과 연결된 itemSlot를 누르면 DragAndDrop으로 전송하여, 항목을 배치
    public void OnPointerClick(PointerEventData eventData)
    {
        //ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>(); 
        itemPanel.OnClick(myIndex);
    }

    public void HighLight(bool b) 
    { 
        highlight.gameObject.SetActive(b);
    } 
}
