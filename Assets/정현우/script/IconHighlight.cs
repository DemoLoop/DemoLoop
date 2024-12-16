using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class IconHighlight : MonoBehaviour
{
    // Vector3: ������ ���, �ε巯�� ������ �� ���� ������ ����
    // Vector3Int: �׸��� ��� �ý����̳� ���� ��ǥ�� �ʿ��� ��쿡 ����

    // CellPosition�� Ÿ�ϸ� ���� Ư�� ���� ��ġ�� ��Ÿ���� ����
    public Vector3Int CellPosition;
    Vector3 targetPosition;
    [SerializeField] Tilemap targetTilemap;
    SpriteRenderer spriteRenderer;

    bool canSelect;
    bool show;


    // CanSelect�� Show ������Ƽ�� ���� ������Ʈ�� Ȱ��ȭ ���¸� �����ϴ� �� ���
    //  canSelect�� show�� ��� true�� ���� ���� ������Ʈ�� Ȱ��ȭ�մϴ�.
    //  ��, �������� ���� �����ϰ� ������ �� ���� ȭ��

    public bool CanSelect
    { 
        set
        {
            canSelect = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    public bool Show
    {
        set 
        {
            show = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    private void Update()
    {
        // CellToWorld �޼���� �־��� ���� �׸��� ��ǥ�� ���� ������ ��ǥ�� ��ȯ
        targetPosition = targetTilemap.CellToWorld(CellPosition);

        // cellSize�� 2�� ������ ���� ���� �߽��� ã�� ���� ���.
        // Ÿ���� ũ�⸦ ������ ������, Ÿ���� �߽� ��ǥ�� ���� �� ����.
        // ���� ���, �� ũ�Ⱑ (1, 1)�̶��, (0.5, 0.5)�� ��
        // targetPosition�� ���� �߽��� ���Ͽ�, ���� ������Ʈ�� Ÿ���� �߾ӿ� ��ġ�ϵ��� �����ϴ� ��
        transform.position = targetPosition + targetTilemap.cellSize/2;
    }

    internal void Set(Sprite icon)
    {
        if (spriteRenderer == null) { spriteRenderer = GetComponent<SpriteRenderer>(); }

        spriteRenderer.sprite = icon;   
    }
}
