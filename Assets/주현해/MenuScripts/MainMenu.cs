// 작성자 : 주현해

// 10-14 LoadNewGame() 함수에서 로딩창으로 놈어갈 때 오류가 발생해서,
// 아예 로딩창으로 이동하는 코드 삭제 후 바로 인게임 진입하는 코드로 수정 by 정현우
// 10-30 씬 이동할 때 페이드 아웃 추가 by 정현우
// 11-14 수정(다중씬 작업) by 정현우

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image fadeImage; // 페이드 이미지
    public float fadeDuration = 1f; // 페이드 지속 시간

    private void Start()
    {
        // 시작할 때 페이드 인
        StartCoroutine(FadeIn());
    }

    public void OnClickNewGame()
    {
        Debug.Log("게임 시작.");
        StartCoroutine(LoadNewGame());
    }

    // 해당 코드 수정 by 정현우
    private IEnumerator LoadNewGame()
    {
        yield return StartCoroutine(FadeOut());

        // 수정(다중씬 작업) by 정현우
        // 메인 씬을 로드
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        // 추가 씬을 로드
        SceneManager.LoadScene("Essential", LoadSceneMode.Additive);

        yield return StartCoroutine(FadeIn());
    }

    public void OnClickLoad()
    {
        Debug.Log("불러오기 클릭됨");
        StartCoroutine(LoadSceneWithFade("Load Screen"));
    }

    public void OnClickOptions()
    {
        Debug.Log("옵션 클릭됨");
        StartCoroutine(LoadSceneWithFade("Options Screen"));
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }


    // 10-30 씬 이동할 때 페이드 아웃 추가 by 정현우
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}