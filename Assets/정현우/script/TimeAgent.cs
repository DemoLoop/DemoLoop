// 2024-10-06, 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick; // 델리게이트, invoke는 여러 메서드를 연결하여 나중에 호출할 수 있는 기능

    private void Start()
    {
        Init();  
    }

    public void Init()
    {
        //TimeAgent가 게임의 시간 시스템에 연결되어 상태 변화를 감지할 수 있도록 설정하는 초기화
        GameManager.Instance.TimeController.Subscribe(this);
    }

    public void Invoke()
    {
        onTimeTick?.Invoke();
        // onTimeTick이 null이 아닐 때만 Invoke()를 호출
        // 즉, 구독자가 없는 경우에는 아무 것도 호출하지 않고 안전하게 넘김
        // onTimeTick은 시간의 변화에 반응하는 메서드를 연결하여, 시간 관련 이벤트를 처리하는 데 사용
    }

    private void OnDestroy()
    {
        GameManager.Instance.TimeController.UnSubscribe(this);
    }
}
