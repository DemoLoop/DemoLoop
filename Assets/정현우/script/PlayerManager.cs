using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MovingObject
{
    static public PlayerManager instance;

    //public int Money;

    public TextMeshProUGUI MeneyPanel;

    public GameObject TopPanel;

    // 대쉬할 때의 속도
    public float runSpeed = 4;
    // 실제 적용되는 대쉬 속도
    private float applyRunSpeed;
    // 대쉬 중인지 여부를 저장하는 변수
    private bool applyRunFlag = false;
    // 캐릭터가 움직일 수 있는지 여부를 저장하는 변수
    public bool canMove = true;

    public bool notMove = false;

    // 비행 가능 여부
    public bool isFlying = false;

    public GameObject toolBarPanel;
    public GameObject inventory;

    public Image flyImage;


    // 게임 시작 시 호출되는 함수 (초기화)
    private void Start()
    {
        queue = new Queue<string>();
        // 캐릭터의 콜라이더와 애니메이터를 가져옴
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // 캐릭터가 움직이는 동작을 처리하는 함수
    IEnumerator MoveCoroutine()
    {
        // 플레이어가 움직이는 방향키를 누르고 있을 때 실행
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 && !notMove)
        {
            // 만약 왼쪽 Shift키가 눌리면 대쉬 속도를 적용
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed; // 대쉬 속도 적용
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0; // 대쉬 안함
                applyRunFlag = false;
            }

            // 플레이어의 이동 방향을 가져옴 (x, y 좌표)
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            // 상하, 좌우 동시에 눌렀을 때 상하 움직임 무시 (이상한 움직임 방지)
            if (vector.x != 0)
                vector.y = 0;

            // 애니메이션에서 방향을 설정 (x축, y축)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            if (isFlying == false)
            {
                bool checkCollsionFlag = base.CheckCollsion();
                if (checkCollsionFlag) { break; } // 박스콜라이더가 있으면 움직이지 않는다.
            }
           

            // 이동 중이면 애니메이션 실행
            animator.SetBool("Walking", true);

            // 이동할 거리만큼 걸을 때까지 반복
            while (currentWalkCount < walkCount)
            {
                // x축으로 이동
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed) / 100, 0, 0);
                }
                // y축으로 이동
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed) / 100, 0);
                }

                // 대쉬 중일 경우 더 빠르게 걷도록 카운트 증가
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;

                // 잠깐 멈추고 다시 실행 (0.01초마다)
                yield return new WaitForSeconds(0.01f);
            }

            // 이동이 끝나면 현재 걸음 수를 초기화
            currentWalkCount = 0;
        }

        // 이동이 끝나면 걷기 애니메이션 중지
        animator.SetBool("Walking", false);
        // 다시 움직일 수 있게 설정
        canMove = true;
    }

    // 매 프레임마다 호출되는 함수 (키 입력 체크)
    private void Update()
    {
        //MeneyPanel.text = "$"+Money.ToString();

        // 캐릭터가 움직일 수 있는 상태에서
        if (canMove && !notMove)
        {
            // 플레이어가 이동 키를 누르면 이동 코루틴 실행
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false; // 움직이는 동안은 다른 입력을 받지 않음
                StartCoroutine(MoveCoroutine()); // 이동 시작
            }
        }


        if (Input.GetKeyDown(KeyCode.F1) && !inventory.activeInHierarchy && !TopPanel.activeInHierarchy)
        {
            isFlying = !isFlying; // 비행 상태 토글
            animator.SetBool("fly", isFlying); // 비행 애니메이션 설정

            if (isFlying)
            {
                flyImage.color = new Color32(255, 255, 255, 255);
                toolBarPanel.SetActive(false);  
                boxCollider.enabled = false; // 비행 중에는 콜라이더 비활성화
                runSpeed = 12; // 비행 시 속도를 12로 설정
                SetSortingLayer("fly");
            }
            else
            {
                flyImage.color = new Color32(125, 125, 125, 255);
                toolBarPanel.SetActive(true);
                boxCollider.enabled = true; // 비행이 아닐 때 콜라이더 활성화
                runSpeed = 4; // 비행이 아닐 때 속도를 4로 설정
                SetSortingLayer("object_1");
            }
        }
    }

    private void SetSortingLayer(string layerName)
    {
        // 스프라이트 렌더러를 가져옵니다.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = layerName; // 정렬 레이어 설정
        }
    }
}
