using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ToolbarController: 툴바의 도구를 관리하는 클래스
public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12; // 툴바의 크기 (도구의 개수)
    int selectedTool; // 현재 선택된 도구의 인덱스

    public Action<int> onChange; // 도구가 변경될 때 호출되는 델리게이트

    [SerializeField] IconHighlight iconHighlight;

    // 현재 선택된 도구의 아이템을 가져오는 프로퍼티
    public item GetItem
    {
        get
        {
            // 현재 선택된 도구의 아이템을 반환
            return GameManager.Instance.inventoryContainer.slots[selectedTool].item;
        }
    }

    private void Start()
    {
        onChange += UpdateHighlightIcon;
        UpdateHighlightIcon(selectedTool);
    }

    private void Update()
    {
        {
            // 숫자 키 입력 감지
            for (int i = 0; i < toolbarSize; i++)
            {
                if (i == 9) // 0번 키는 특별히 처리
                {
                    if (Input.GetKeyDown(KeyCode.Alpha0))
                    {
                        selectedTool = i;
                        onChange?.Invoke(selectedTool);
                        return;
                    }
                }
                else // 1~9번 키 처리
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    {
                        selectedTool = i;
                        onChange?.Invoke(selectedTool);
                        return;
                    }
                }
            }
        }
    }

    /*
    // 매 프레임마다 호출되는 함수
    private void Update()
    {
        // 마우스 스크롤의 변화량을 가져옴
        float delta = Input.mouseScrollDelta.y;

        // 스크롤이 움직였을 때
        if (delta != 0)
        {
            // 스크롤이 위로 움직이면
            if (delta > 0)
            {
                selectedTool += 1; // 선택된 도구를 하나 증가
                // 선택된 도구가 툴바 크기를 초과하면 처음으로 돌아감
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            // 스크롤이 아래로 움직이면
            else
            {
                selectedTool -= 1; // 선택된 도구를 하나 감소
                // 선택된 도구가 0보다 작으면 마지막 도구로 돌아감
                selectedTool = (selectedTool < 0 ? toolbarSize - 1 : selectedTool);
            }
            // 도구가 변경되었음을 알림
            onChange?.Invoke(selectedTool);
        }
    }*/

    // 외부에서 선택된 도구의 인덱스를 설정하는 함수
    internal void Set(int id)
    {
        selectedTool = id; // 선택된 도구를 주어진 id로 설정
    }

    void UpdateHighlightIcon(int id)
    {
        item item = GetItem;
        if (item == null)
        {
            iconHighlight.Show = false;
            return;
        }

        iconHighlight.Show = item.iconHighlight;
        if (item.iconHighlight)
        {
            iconHighlight.Set(item.icon);
        }
    }
}