using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// DialogueSystem: 게임 속 대화를 처리하는 시스템
public class DialogueSystem : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager; // PlayerManager를 외부에서 설정할 수 있도록

    // 대화가 진행 중인지 여부
    public bool IsActive = false;

    // 대화 내용이 출력될 텍스트 (TextMeshProUGUI는 유니티의 텍스트 렌더링 컴포넌트)
    [SerializeField] TextMeshProUGUI targetText;

    // 대화 상대의 이름이 출력될 텍스트
    [SerializeField] TextMeshProUGUI nameText;

    // 대화 상대의 초상화 이미지
    [SerializeField] Image portrait;

    // 현재 진행 중인 대화 내용(문장들이 담긴 대화 컨테이너)
    DialogueContainer currentDialogue;

    // 현재 출력 중인 대화 줄의 번호
    int currentTextLine;

    // 텍스트가 얼마나 출력되었는지를 백분율로 나타냄 (0% ~ 100%)
    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;

    // 각 글자가 표시되는 속도 (글자당 딜레이)
    [SerializeField] float timePerLetter = 0.05f;

    // 전체 대사를 출력하는 데 걸리는 시간과 현재 경과한 시간
    float totalTimetoType, currentTime;

    // 출력할 대사 줄
    string lineToShow;

    // 매 프레임마다 실행되는 함수
    private void Update()
    {
        // 스페이스바를 눌렀을 때 대사를 넘김
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PushText();
        }

        // ESC 키를 눌렀을 때 대화창을 닫음
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Conclude();
        }

        // 현재 대사가 출력되는 중이면 계속해서 텍스트를 출력
        TypeOutText();
    }

    // 대사를 한 글자씩 타이핑 효과로 출력하는 함수
    private void TypeOutText()
    {
        // 만약 대사가 이미 다 출력되었으면 아무 것도 하지 않음
        if (currentTime >= 1f) { return; }

        // 시간이 지나면서 대사 출력 진행도를 계산
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimetoType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0, 1f); // 0%에서 100% 사이로 제한
        UpdateText(); // 텍스트 업데이트
    }

    // 대사 텍스트를 업데이트하는 함수 (일부만 출력)
    void UpdateText()
    {
        // 현재 출력할 글자 수를 계산
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
        // 계산된 글자 수만큼 텍스트를 잘라서 표시
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    // 스페이스바를 눌렀을 때 호출되는 함수
    private void PushText()
    {
        // 만약 대사가 아직 다 출력되지 않았으면, 남은 대사를 한 번에 출력
        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }

        // 대화를 종료
        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        }
        else
        {
            // 아직 남은 대사가 있으면 다음 줄로 넘어감
            CycleLine();
        }
    }

    // 다음 대사 줄을 설정하는 함수
    void CycleLine()
    {
        // 현재 대사 줄을 가져옴
        lineToShow = currentDialogue.line[currentTextLine];

        // 글자 수에 따라 출력 시간을 계산
        totalTimetoType = lineToShow.Length * timePerLetter;

        // 타이머 초기화 및 텍스트 비우기
        currentTime = 0f;
        visibleTextPercent = 0f;
        targetText.text = "";

        // 다음 줄로 넘어감
        currentTextLine += 1;
    }

    // 대화를 초기화하는 함수 (대화 시작 시 호출)
    public void Initialize(DialogueContainer dialogueContainer)
    {
        IsActive = true;

        playerManager.notMove = true; // 플레이어의 이동을 멈춤
        playerManager.canMove = false;

        Show(true); // 대화창을 보이게 설정
        currentDialogue = dialogueContainer; // 대사 데이터 설정
        currentTextLine = 0; // 첫 번째 줄부터 시작
        CycleLine(); // 첫 대사 줄 출력
        UpdatePortrait(); // 캐릭터 초상화 및 이름 업데이트
    }

    // 캐릭터의 초상화와 이름을 업데이트하는 함수
    private void UpdatePortrait()
    {
        // 대사에 있는 캐릭터의 초상화와 이름을 가져와 설정
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.Name;
    }

    // 대화창을 보이게 하거나 숨기는 함수
    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }

    // 대화가 끝났을 때 호출되는 함수
    private void Conclude()
    {
        IsActive = false;
        Debug.Log("The dialogue has ended"); // 디버그 메시지 출력
        Show(false); // 대화창 숨기기

        playerManager.notMove = false; // 플레이어의 이동을 허용
        playerManager.canMove = true;
    }
}