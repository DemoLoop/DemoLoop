// 2024-09-07 �ۼ��� : ������

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GridBrushBase;

public class ToolCharacterController : MonoBehaviour
{
    // ĳ������ �������� ����ϴ� ��ũ��Ʈ 12/02
    PlayerMovement character;

    // ��ư �Է� ��Ÿ�� ���� 12/02
    [SerializeField] private float inputCooldown = 1f; // ��Ÿ�� ���� �ð� (��)
    [SerializeField] private float fishingInputCooldown = 0.75f; // ��Ÿ�� ���� �ð� (��)
    [SerializeField] private float MasterFishingInputCooldown = 0.1f; // ��Ÿ�� ���� �ð� (��)
    private float lastInputTime = -1f; // ������ �Է� �ð� (-1�� �ʱⰪ)

    // ���� Ȱ��ȭ ���� 12/02
    [SerializeField] GameObject storePanel;
    // �κ��丮 Ȱ��ȭ ���� 12/02
    [SerializeField] GameObject inventory;

    PlayerManager playerManager;

    Rigidbody2D rgdb2d;

    // ������ �����ϰ� ����ϴ� ToolbarController
    ToolbarController toolbarController;

    Animator animator;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1f;

    // ��ȣ�ۿ��� �� �ִ� Ÿ�ϰ� ������Ʈ�� ǥ���ϴ� MarkerManager
    [SerializeField] MarkerManager markerManager;

    // Ÿ�� ������ �о���� TileMapReadController
    [SerializeField] TileMapReadController tileMapReadController;

    // ĳ���Ͱ� ��ȣ�ۿ��� �� �ִ� �ִ� �Ÿ�
    [SerializeField] float maxDistance = 1.5f;

    // Ÿ���� �������� �� ����Ǵ� �׼�

    [SerializeField] ToolAction onTilePickUp; // (12-8��) ���� ���콺 ��ư�� ������ ��, ��Ȯ �����ϱ� ���ؼ�, 186�� �ּ�ó��

    // ��ȭâ (Ȱ��ȭ ���ο� ���� �ٸ� ������ �ϱ� ���� ���)
    [SerializeField] GameObject dialougePanel; 

    [SerializeField] TileData plowableTiles;

    [SerializeField] IconHighlight IconHighlight;

    // ĳ���Ͱ� ��ȣ�ۿ��� Ÿ���� ��ġ
    Vector3Int selectedTilePosition;

    // ĳ���Ͱ� Ÿ���� ������ �� �ִ��� ���� (�Ÿ��� ���� ����)
    bool selectable;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        character = GetComponent<PlayerMovement>(); 
        rgdb2d = GetComponent<Rigidbody2D>();    
        toolbarController = GetComponent<ToolbarController>();
        animator = GetComponent<Animator>();
        //characterAppearance = GetComponent<CharacterAppearance>();
    }
    private void Update()
    {
        // Ÿ�� ���� �� Ȯ�� ����
        SelectTile();
        CanSelectedCheck();
        Marker();

        item item = toolbarController?.GetItem;

        if (item?.Name != null)
        {
            // �Է� ��Ÿ�� üũ
            if (item.Name.Contains("fishing") && item.Name != "fishing07")
            {
                if (Time.time - lastInputTime < fishingInputCooldown)
                {
                    return; // ��Ÿ�� ���̸� �Է��� ����
                }
            }
            else if (item.Name == "fishing07")
            {
                if (Time.time - lastInputTime < MasterFishingInputCooldown)
                {
                    return; // ��Ÿ�� ���̸� �Է��� ����
                }
            }
            else
            {
                if (Time.time - lastInputTime < inputCooldown)
                {
                    return; // ��Ÿ�� ���̸� �Է��� ����
                }
            }
        }

        // ��ư �Է� Ȯ��
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            && !dialougePanel.activeInHierarchy && !playerManager.isFlying 
            && !storePanel.activeInHierarchy && !inventory.activeInHierarchy)
        {
            // ������ �Է� �ð� ����
            lastInputTime = Time.time;

            // ���� ��� ����
            if (UseToolWorld())
            {
                return;
            }
            UseToolGrid();
        }

        if ((Input.GetMouseButtonDown(1)))
        {
            PickUptile();
        }
    }

    // ���콺 ��ġ�� �ش��ϴ� Ÿ���� �����ϴ� �Լ�
    private void SelectTile()
    {
        // ���콺�� ��ġ�� ���� Ÿ�ϸ� ��ǥ�� ������
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }

    // ĳ���Ͱ� Ÿ���� ������ �� �ִ��� Ȯ���ϴ� �Լ�
    void CanSelectedCheck()
    {
        // ĳ���Ϳ� ���콺 ��ġ ���� �Ÿ��� ���
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;

        // ���� ���� ���ο� ���� ��Ŀ ǥ��
        markerManager.Show(selectable);
        IconHighlight.CanSelect = selectable;

    }

    // ��Ŀ(Ÿ���� ��ġ�� �ð������� ǥ��)�� �����ϴ� �Լ�
    private void Marker()
    {
        markerManager.markedCellPostion = selectedTilePosition;
        IconHighlight.CellPosition = selectedTilePosition;
    }

    // ���� �󿡼� ������ ����ϴ� �Լ�
    private bool UseToolWorld() 
    {
        // ĳ���Ͱ� ���������� ������ ���⿡ offsetDistance��ŭ ������ ��ġ���� ��ȣ�ۿ�
        Vector2 position = rgdb2d.position + character.lastmotionVector * offsetDistance;

        // ���� ���õ� ����(������)�� ������
        item item = toolbarController.GetItem;

        // ���õ� ������ ������ ����
        if (item == null) { return false; }

        // ������ �Ҵ�� �׼��� ������ ����
        if (item.onAction == null) { return false; }

        // ������ ����ϴ� �ִϸ��̼� ����
        if (item.Name == "fishing01" || item.Name == "fishing02" || item.Name == "fishing03" ||
            item.Name == "fishing04" || item.Name == "fishing05" || item.Name == "fishing06" ||
            item.Name == "fishing07")
        {
            animator.SetTrigger("fishing");
        }
        else if(item.Name == "Hunt" || item.Name == "Hunt1")
        {
            animator.SetTrigger("act2");
        }
        else if (item.Name.Contains("Axe") || item.Name.Contains("Mine") 
            || item.Name.Contains("HarvestItem") || item.Name.Contains("Plow"))
        {
            animator.SetTrigger("act");
        }
        else { };

        // ������ �׼��� ����
        bool complete = item.onAction.OnApply(position);

        // ���� ����� �Ϸ�Ǹ�
        if (complete == true)
        {
            if (item.onItemUsed != null)
            {
                // ������ ���Ǿ����� �κ��丮�� �ݿ�
                item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
            }
        }

        return complete;
    }

    // Ÿ�Ͽ��� ������ ����ϴ� �Լ�
    private void UseToolGrid()
    {
        if (selectable == true)
        {
            // toolbarController���� ���� ���õ� �������� ������,
            // �������� null�� ���, �� ���õ� �������� ������ �޼��带 ����
            item item = toolbarController.GetItem;
            if (item == null) {
                //PickUptile(); ��Ȯ ����(12-8��)
                return; 
            }

            // �������� onTileMapAction�� null�� ���, �� Ÿ�� �ʿ� ������ �� ���� �������̸� �޼��带 ����
            if (item.onTileMapAction == null) { return; }

            if (item.Name.Contains("Axe") || item.Name.Contains("Mine")
            || item.Name.Contains("HarvestItem") || item.Name.Contains("Plow"))
            {
                animator.SetTrigger("act");
            }

            // �������� onTileMapAction�� ȣ���Ͽ� ���õ� Ÿ�� ��ġ�� �������� ����
            bool complete = item.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController, item);

            // ���� ����� �Ϸ�Ǹ�
            if (complete == true)
            {
                if(item.onItemUsed != null)
                {
                    // ������ ���Ǿ����� �κ��丮�� �ݿ�
                    item.onItemUsed.OnItemUsed(item, GameManager.Instance.inventoryContainer);
                }
                
            }
            
        }
    }

    // Ÿ���� �����ϴ� �Լ�
    private void PickUptile()
    {
        if (onTilePickUp == null) { return; }

        onTilePickUp.OnApplyToTileMap(selectedTilePosition, tileMapReadController, null);
    }
}
