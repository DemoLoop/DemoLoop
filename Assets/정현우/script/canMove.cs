using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canMove : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그에 닿을 때
        {
            StartCoroutine(EnableMovementAfterDelay());
        }
    }

    private IEnumerator EnableMovementAfterDelay()
    {
        // 1초 대기
        yield return new WaitForSeconds(0.35f);

        // 이동 가능 설정
        playerManager.canMove = true;
        playerManager.notMove = false;

        yield return new WaitForSeconds(0.01f);

        // 객체 파괴
        Destroy(gameObject);
    }
}