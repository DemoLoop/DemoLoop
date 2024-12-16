// 2024-09-09, 10-06, 작성자 : 정현우


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour
{
    // 하루에 해당하는 시간(초 단위, 86400초 = 24시간)
    const float secondsInDay = 86400f;

    // 15분마다 한 번씩 발생하는 시간 단위
    const float phaseLength = 900f; // 15 minutes chunk of time

    // 밤 시간대의 조명 색상
    [SerializeField] Color nightLightColor;

    // 시간에 따른 조명의 밝기 변화를 조절하는 곡선
    [SerializeField] AnimationCurve nightTimeCurve;

    // 낮 시간대의 조명 색상 (기본적으로 흰색)
    [SerializeField] Color dayLightColor = Color.white;

    // 게임 내 시간
    public float time;

    // 현실 시간에 비해 게임 시간이 빨리 흐르도록 설정 (1초 = 게임 내 60초)
    [SerializeField] float timeScale = 60f;

    // 게임 시작 시의 시간을 설정 (28800초 = 오전 8시)
    [SerializeField] float startAtTime = 28800f;

    // 시간 정보를 표시할 텍스트 UI
    [SerializeField] TextMeshProUGUI text;

    // 전반적인 조명을 관리하는 2D 라이트
    [SerializeField] Light2D globalLight;

    // 지나간 날 수를 저장
    private int days;

    // 시간에 반응하는 객체들의 리스트 (시간에 따라 동작하는 것들)
    List<TimeAgent> agents;

    // 스크립트가 처음 실행될 때 호출되는 함수
    private void Awake()
    {
        // 시간에 반응하는 객체들을 저장할 리스트를 초기화
        agents = new List<TimeAgent>();
    }

    // 게임이 시작될 때 호출되는 함수
    private void Start()
    {
        // 게임 시작 시 설정된 시간에서 시작
        time = startAtTime;
    }

    // 새로운 TimeAgent를 리스트에 추가하는 함수
    public void Subscribe(TimeAgent timeagent)
    {
        agents.Add(timeagent);
    }

    // 기존 TimeAgent를 리스트에서 제거하는 함수
    public void UnSubscribe(TimeAgent timeagent)
    {
        agents.Remove(timeagent);
    }

    // 현재 시간을 시간 단위로 반환 (초 -> 시간으로 변환)
    float Hours
    {
        get { return time / 3600f; }
    }

    // 현재 시간을 분 단위로 반환 (초 -> 분으로 변환)
    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    // 물리 연산과 관련된 프레임마다 호출되는 함수
    private void FixedUpdate()
    {
        // 시간 경과에 따라 게임 내 시간을 증가 (현실 시간과 비율을 맞춤)
        time += Time.deltaTime * timeScale;

        // 시간 값을 계산하고 UI에 표시
        TimeValueCalculation();

        // 시간에 따라 낮과 밤의 조명을 변경
        DayLight();

        // 하루가 지났을 경우 다음 날로 넘어감
        if (time > secondsInDay)
        {
            NextDay();
        }

        // 15분마다 시간에 반응하는 객체들을 실행
        TimeAgents();
    }

    // 시간 값(시, 분)을 계산하고 UI 텍스트에 표시하는 함수
    private void TimeValueCalculation()
    {
        int hh = (int)Hours; // 시간을 정수형으로 변환
        int mm = (int)Minutes; // 분을 정수형으로 변환
        // 시간을 "00:00" 형식으로 텍스트에 표시
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
    }

    // 시간에 따라 낮과 밤의 조명 색상을 계산하여 적용하는 함수
    private void DayLight()
    {
        // 시간에 따라 조명의 밝기 정도를 계산
        float v = nightTimeCurve.Evaluate(Hours);

        // 낮과 밤의 색상을 섞어서 현재 시간대에 맞는 색상을 계산
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);

        // 조명의 색상을 적용
        globalLight.color = c;
    }

    // 이전 시간대의 단위(15분) 저장
    int oldPhase = 0;

    // 15분마다 agents 리스트의 모든 객체들에게 시간을 알려주는 함수
    private void TimeAgents()
    {
        // 현재 시간을 15분 단위로 나눈 값 계산
        int currentPhase = (int)(time / phaseLength);

        // 만약 시간이 15분이 지나면(새로운 단위로 넘어가면)
        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase; // 이전 시간대를 갱신
            // 리스트에 있는 모든 객체들의 시간을 업데이트
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke(); // 시간에 따라 실행해야 할 동작을 호출
            }
        }
    }

    // 하루가 지나면 시간을 0으로 리셋하고 날 수를 증가시키는 함수
    private void NextDay()
    {
        time = 0; // 시간을 0으로 리셋
        days += 1; // 지나간 날 수를 증가
    }

    private void Update()
    {
        // + 키를 누르면 timeScale을 100 증가
        if (Input.GetKeyDown(KeyCode.Equals)) // '=' 키로 감지 (Shift와 함께 '+' 입력됨)
        {
            timeScale += 100f;
        }

        // - 키를 누르면 timeScale을 100 감소
        if (Input.GetKeyDown(KeyCode.Minus)) // '-' 키로 감지
        {
            timeScale -= 100f;
            // timeScale이 음수가 되지 않도록 보정
            if (timeScale < 0f)
            {
                timeScale = 0f;
            }
        }
    }
}