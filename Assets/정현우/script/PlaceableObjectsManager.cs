//�ۼ��� : ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : MonoBehaviour
{
    [SerializeField] PlaceableObjectsContainer placeableObjects;
    [SerializeField] Tilemap targetTilemap;

    private void Start()
    {
        GameManager.Instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
    }

    // �־��� �������� Ư�� ��ġ�� ��ġ�ϴ� ����� ������
    public void Place(item item, Vector3Int positionOnGrid)
    {
        // �־��� �������� ������[�ڻ�(asset)]�� �ν��Ͻ�ȭ�Ͽ� ���ο� ���� ������Ʈ�� ����
        GameObject go = Instantiate(item.itemPrefeb);
        // Ÿ�ϸ��� ���� ��ǥ�� ���� ��ǥ�� ��ȯ
        Vector3 position = targetTilemap.CellToWorld(positionOnGrid) + targetTilemap.cellSize/2;
        position -= Vector3.forward * 0.1f;
        go.transform.position = position;
        // ���ο� PlaceableObject �ν��Ͻ��� placeableObjects �����̳ʿ� �߰��մϴ�.
        // �� �κ��� ��ġ�� ��ü�� �����ϱ� ���� ��
        placeableObjects.placeableObjects.Add(new PlaceableObject(item, go.transform, positionOnGrid));
    }
}
