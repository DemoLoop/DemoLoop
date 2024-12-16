using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressBar;

    // 씬 로드 메서드
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading Bar"); // 로딩 씬으로 이동
    }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        // 다음 씬을 비동기로 로드
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        // 로딩이 완료되지 않을 때까지 반복
        while (!op.isDone)
        {
            yield return null;

            // 로딩 진행률 업데이트
            if (op.progress < 0.9f)
            {
                //progressBar.fillAmount = op.progress; // 진행률 표시
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer); // 마지막 진행률 부드럽게 전환
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true; // 씬 전환 허용
                }
            }
        }
    }
}