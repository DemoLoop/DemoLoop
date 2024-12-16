// 2024-9-15, 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 움직이는 기능을 처리하는 클래스
public class MovingObject : MonoBehaviour
{
    public string characterName;

    // 캐릭터가 움직이는 속도
    public float speed;
    // 한 번에 몇 걸음씩 이동할 것인지 결정
    public float walkCount;
    // 현재 걸은 횟수
    protected int currentWalkCount;

    private bool notCoroutine = false;

    // 캐릭터가 움직일 방향을 저장하는 벡터
    protected Vector3 vector;  // protected 부모 자식은 참조가 가능하지만, 외부에서 인스텍터에서 접근이 불가능함

    //선입선출 구조
    public Queue<string> queue;
    // 캐릭터가 부딪힐 수 있는 충돌 박스 (콜라이더)
    public BoxCollider2D boxCollider;
    // 캐릭터가 충돌할 레이어를 설정 (벽이나 장애물)
    public LayerMask layerMask;
    // 캐릭터의 애니메이션을 제어하는 컴포넌트
    public Animator animator;

    public void Move(string _dir, int _freqency = 5)  // int _freqency = 5, 인수를 넘겨도 값을 생략할 수 있게 만듬
    {
        queue.Enqueue(_dir);
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _freqency));
        }
    }

    IEnumerator MoveCoroutine(string _dir, int _freqency)
    {
        while (queue.Count != 0)
        {
            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);


            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * (speed), vector.y * (speed), 0);
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

            currentWalkCount = 0;
            if (_freqency != 5)
                animator.SetBool("Walking", false);
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;
    }

    protected bool CheckCollsion() 
    {
        RaycastHit2D hit;
        Vector2 start = transform.position; // 캐릭터 현재 위치
        Vector2 end = start + new Vector2(vector.x * speed * walkCount / 30, vector.y * speed * walkCount / 60);
        // 이동할 목표 위치

        // 캐릭터의 콜라이더를 비활성화 (자기 자신과 충돌하지 않기 위해)
        boxCollider.enabled = false;
        // 레이캐스트로 이동할 경로에 장애물이 있는지 확인
        hit = Physics2D.Linecast(start, end, layerMask);
        // 콜라이더를 다시 활성화
        boxCollider.enabled = true;

        // 만약 충돌하는 객체가 있으면 이동 중단
        if (hit.transform != null)
            return true;
        return false;
    }
}
