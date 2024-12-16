using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ToolbarController: ������ ������ �����ϴ� Ŭ����
public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12; // ������ ũ�� (������ ����)
    int selectedTool; // ���� ���õ� ������ �ε���

    public Action<int> onChange; // ������ ����� �� ȣ��Ǵ� ��������Ʈ

    [SerializeField] IconHighlight iconHighlight;

    // ���� ���õ� ������ �������� �������� ������Ƽ
    public item GetItem
    {
        get
        {
            // ���� ���õ� ������ �������� ��ȯ
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
            // ���� Ű �Է� ����
            for (int i = 0; i < toolbarSize; i++)
            {
                if (i == 9) // 0�� Ű�� Ư���� ó��
                {
                    if (Input.GetKeyDown(KeyCode.Alpha0))
                    {
                        selectedTool = i;
                        onChange?.Invoke(selectedTool);
                        return;
                    }
                }
                else // 1~9�� Ű ó��
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
    // �� �����Ӹ��� ȣ��Ǵ� �Լ�
    private void Update()
    {
        // ���콺 ��ũ���� ��ȭ���� ������
        float delta = Input.mouseScrollDelta.y;

        // ��ũ���� �������� ��
        if (delta != 0)
        {
            // ��ũ���� ���� �����̸�
            if (delta > 0)
            {
                selectedTool += 1; // ���õ� ������ �ϳ� ����
                // ���õ� ������ ���� ũ�⸦ �ʰ��ϸ� ó������ ���ư�
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            // ��ũ���� �Ʒ��� �����̸�
            else
            {
                selectedTool -= 1; // ���õ� ������ �ϳ� ����
                // ���õ� ������ 0���� ������ ������ ������ ���ư�
                selectedTool = (selectedTool < 0 ? toolbarSize - 1 : selectedTool);
            }
            // ������ ����Ǿ����� �˸�
            onChange?.Invoke(selectedTool);
        }
    }*/

    // �ܺο��� ���õ� ������ �ε����� �����ϴ� �Լ�
    internal void Set(int id)
    {
        selectedTool = id; // ���õ� ������ �־��� id�� ����
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