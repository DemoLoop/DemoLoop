using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionArea : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    private bool Bool = false;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }


    // OnTriggerEnter2D는 Unity에서 2D 물리 시스템을 사용할 때, 트리거 충돌이 발생했을 때 호출되는 메서드
    // 특히 두 오브젝트 중 하나가 트리거로 설정되어 있을 때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Bool)
        {
            Bool = true;
            playerManager.canMove = false;
            playerManager.notMove = true;

            if (collision.transform.CompareTag("Player"))
            {
                // 충돌한 오브젝트의 부모 오브젝트에서 Transition 컴포넌트를 가져옵니다
                transform.parent.GetComponent<Transition>().InitiateTransition(collision.transform);
                Bool = false;
            }
        }
        
    }
}
