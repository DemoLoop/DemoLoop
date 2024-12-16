// 2024-10-06 작성자: 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeAgent))]

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] item toSpawn;
    [SerializeField] int count;

    [SerializeField] float spread = 2f;

    [SerializeField] float probability = 0.5f;

    private void Start()
    {
        TimeAgent timeAgent = GetComponent<TimeAgent>();
        timeAgent.onTimeTick += Spawn;
    }

    void Spawn()
    {
        if(UnityEngine.Random.value < probability)
        { 
        // 랜덤한 위치에 아이템 출현;
        Vector3 position = transform.position;
        position.x += spread * UnityEngine.Random.value - spread / 2;
        position.y += spread * UnityEngine.Random.value - spread / 2;

        itemSpawnManager.instance.SpawnItem(position, toSpawn, count);
        }
    }
}
