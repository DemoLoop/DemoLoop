// 2024-09-19 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawnManager : MonoBehaviour
{
    public static itemSpawnManager instance; // 싱글턴 패턴을 위한 인스턴스 변수

    private void Awake()
    {
        instance = this; // 현재 인스턴스를 정적 변수에 할당, 싱글턴 패턴 구현
    }

    [SerializeField] GameObject pickUpItemPrefab; // 생성할 픽업 아이템의 프리팹

    // 위치, 아이템, 아이템 수를 받아서 아이템을 생성하는 함수
    public void SpawnItem(Vector3 position, item item, int count)
    {
        // 지정된 위치에 픽업 아이템 프리팹을 인스턴스화
        GameObject o = Instantiate(pickUpItemPrefab, position, Quaternion.identity);

        // 생성된 오브젝트에 아이템과 개수를 설정
        o.GetComponent<PickUpItem>().Set(item, count);
    }
}