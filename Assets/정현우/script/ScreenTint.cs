using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField]
    Color unTintedColor;
    [SerializeField]
    Color tintedColor;
    float f;
    public float speed = 0;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Tint()
    {
        StopAllCoroutines();
        f = 0f; // 0은 원래 색상, 1은 변화된 색상
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        StopAllCoroutines();
        f = 0f; // 0은 원래 색상, 1은 변화된 색상
        StartCoroutine(UnTintScreen());
    }

    private IEnumerator TintScreen()
    {
        while (f < 1f)
        {
            // 프레임 간의 시간 차이를 나타내며, 이를 통해 f를 점진적으로 증가
            f += Time.deltaTime * speed; 
            // f의 값을 0과 1 사이로 제한합니다. 이렇게 하면 f가 1을 초과하지 않도록 보장
            f = Math.Clamp(f, 0f, 1f);

            Color c = image.color;

            // unTintedColor와 tintedColor 사이의 색상을 f에 비례하여 계산합니다.
            // f가 0일 때는 unTintedColor, 1일 때는 tintedColor가 반환됨. 
            // Color.Lerp는 두 색상 간의 보간을 수행할 때 RGB 값뿐만 아니라 알파 값도 함께 보간 중요
            c = Color.Lerp(unTintedColor, tintedColor, f); // 
            image.color = c;

            // 현재 프레임의 끝까지 기다린 후 다음 루프를 실행하도록 합니다.
            // 이렇게 하면 매 프레임마다 색상이 조금씩 변화
            yield return new WaitForEndOfFrame();
        }
        //UnTint();  //오류인듯
    }

    private IEnumerator UnTintScreen()
    {
        while (f < 1f)
        {
            f += Time.deltaTime * speed;
            f = Math.Clamp(f, 0f, 1f);

            Color c = image.color;
            c = Color.Lerp(tintedColor, unTintedColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }
    }
}
