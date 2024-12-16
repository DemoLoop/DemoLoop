// 2024-09-07 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
// [RequireComponent(typeof(BoxCollider2D))]는 유니티에서 사용하는 특성(Attribute)으로,
// 특정 스크립트가 반드시 BoxCollider2D 컴포넌트를 필요로 한다는 것 의미
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.5f;
    [SerializeField] item item;
    [SerializeField] int itemCountInOneDrop;
    [SerializeField] int dropCount = 5; // 출현되는 아이템 개수; 
    [SerializeField] ResourceNodeType nodeType;

    [SerializeField] Animator animator; // 12-11

    public override void Hit()
    {
        StartCoroutine(HandleHit());
    }

    private IEnumerator HandleHit()
    {
        if (this.gameObject.name.Contains("bunny"))
        {
            animator.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f); // 0.5초 지연
        if (this.gameObject.name.Contains("bunny"))
        {
            animator.gameObject.SetActive(false);
        }

        while (dropCount > 0)
        {
            dropCount -= 1;
            // 랜덤한 위치에 아이템 출현;
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            itemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
        }

        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
