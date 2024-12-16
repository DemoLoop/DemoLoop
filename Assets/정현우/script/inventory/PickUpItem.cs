// 2024-09-07 작성자 : 정현우

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player; //플레이어 위치
    [SerializeField] float speed = 5f; //플레이어를 향해 아이템 객체가 이동하는 속도;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f; // 일정 시간 안에 아이템 안 먹으면 소멸

    public item item;
    public int count = 1;

    private void Awake()
    {
        player = GameManager.Instance.player.transform;
    }

    // 항목과 개수를 전달하고 여기에 저장하는 Set 메소드
    public void Set(item item, int count)
    { 
        this.item = item;
        this.count = count; 

        // 렌더링된 스프라이트를 아이템 아이콘 스프라이트로 교체하는 용도
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }




    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0) { Destroy(gameObject); }

        // 객체와 플레이어 사이의 거리를 변수에 저장
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }
        // 아이템이 플레이어를 향해 이동    
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // 아이템과 플레이어 거리가 0.1f 이하면 아이템 파괴
        if (distance < 0.1f)
        {
            if (GameManager.Instance.inventoryContainer != null)
            {
                GameManager.Instance.inventoryContainer.Add(item, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the GameManager");
            }
            Destroy(gameObject);
        }
    }
}
