// 작성자 : 정현우, 날짜 : 2024-09-04 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public SpriteRenderer white;
    public SpriteRenderer black;
    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public void FadeOut(float _speed = 0.02f) {
        StopAllCoroutines(); // 맵을 왔다갔다하면서 코루틴 중첩 실행하는걸 방지 //마지막 실행된게 우선순위 부여
        StartCoroutine(FadeOutCoroutine(_speed));
    }


    IEnumerator FadeOutCoroutine(float _speed) { 

        color = black.color;
        while (color.a < 1f) { //컬러 블랙의 알파값이 1이 될 때까지
            color.a += _speed;
            black.color = color;
            yield return waitTime; // new는 연산자에 부하를 많이 줌
        }
    }

    public void FadeIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_speed));
    }


    IEnumerator FadeInCoroutine(float _speed)
    {

        color = black.color;
        while (color.a > 0f)
        { //컬러 블랙의 알파값이 0이 될 때까지
            color.a -= _speed;
            black.color = color;
            yield return waitTime; // new는 연산자에 부하를 많이 줌
        }
    }

    public void FlashOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashOutCoroutine(_speed));
    }


    IEnumerator FlashOutCoroutine(float _speed)
    {

        color = white.color;
        while (color.a < 1f)
        { //컬러 블랙의 알파값이 1이 될 때까지
            color.a += _speed;
            white.color = color;
            yield return waitTime; // new는 연산자에 부하를 많이 줌
        }
    }

    public void Flash(float _speed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(_speed));
    }


    IEnumerator FlashCoroutine(float _speed) //번쩍 거리는거
    {

        color = white.color;
        while (color.a < 1f)
        { //컬러 블랙의 알파값이 1이 될 때까지
            color.a += _speed;
            white.color = color;
            yield return waitTime; // new는 연산자에 부하를 많이 줌
        }
        while (color.a > 0)
        { //컬러 블랙의 알파값이 1이 될 때까지
            color.a -= _speed;
            white.color = color;
            yield return waitTime; // new는 연산자에 부하를 많이 줌
        }
    }



    public void FlashIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashInCoroutine(_speed));
    }


    IEnumerator FlashInCoroutine(float _speed)
    {

        color = white.color;
        while (color.a > 0)
        { //컬러 블랙의 알파값이 1이 될 때까지
            color.a -= _speed;
            white.color = color;
            yield return waitTime; // new는 연산자에 부하를 많이 줌
        }
    }

}
;