using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookContainerIntereact : Interactable
{
    // 가상메서드를 상속하고 구현
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;
    [SerializeField] AudioClip onOpenAudio;

    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.5f;

    [SerializeField] item item;
    [SerializeField] item item2;

    [SerializeField] int itemCountInOneDrop;
    [SerializeField] int dropCount = 1; // 출현되는 아이템 개수; 
    [SerializeField] ResourceNodeType nodeType;

    public override void Interact(Character character)
    {
        if (opened == false)
        { 
            opened = true;
            closedChest.SetActive(false);
            openedChest.SetActive(true);
            StartCoroutine(HandleHit());

            AudioManager.instance.Play(onOpenAudio);
        }
    }

    private IEnumerator HandleHit()
    {
        yield return new WaitForSeconds(0.5f); // 0.5초 지연

        while (dropCount > 0)
        {
            dropCount -= 1;
            // 랜덤한 위치에 아이템 출현;
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            itemSpawnManager.instance.SpawnItem(position, item, itemCountInOneDrop);
            itemSpawnManager.instance.SpawnItem(position, item2, itemCountInOneDrop);
        }

        //Destroy(gameObject);
    }
}
