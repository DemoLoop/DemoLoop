// 작성자 정현우
// SwitchScene() 일부 수정 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenceManager : MonoBehaviour
{
    public static GameScenceManager instance; //정적 변수, 클래스의 모든 인스턴스가 공유하는 변수
    [SerializeField] PlayerManager playerManager;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ScreenTint screenTint;
    string currentScence;

    void start() 
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StopAllCoroutines();
        StartCoroutine(Transition(to, targetPosition));
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();
        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);
        SwitchScene(to, targetPosition);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.5f);
        screenTint.UnTint();
    }

    void Start()
    {
        currentScence = SceneManager.GetActiveScene().name;
    }


    public void SwitchScene(string to, Vector3 targetPosition)
    {
        // 로드할 씬이 현재 씬과 같은지 확인
        if (currentScence == to)
        {
            Debug.Log("이미 씬이 로드되어 있습니다: " + to);
            return; // 씬이 이미 로드되어 있으면 메서드를 종료
        }

        // 지정된 씬(to)을 Additive 모드로 로드
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        // 씬 로드가 완료될 때까지 대기
        loadOperation.completed += (AsyncOperation op) =>
        {
            // 현재 활성화된 씬(currentScence)을 비동기적으로 언로드(제거)
            SceneManager.UnloadSceneAsync(currentScence);

            // currentScence 변수를 업데이트하여,
            // 현재 씬의 이름을 새로 전환한 씬의 이름으로 변경
            currentScence = to;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScence));

            // 플레이어를 타겟 위치로 이동
            Transform playerTransform = GameManager.Instance.player.transform;
            playerTransform.position = new Vector3(targetPosition.x, targetPosition.y, playerTransform.position.z);
            PlayerManager playerManager = GameManager.Instance.player.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.notMove = false;
                playerManager.canMove = true;
            }
            else
            {
                Debug.LogError("PlayerManager 컴포넌트를 찾을 수 없습니다.");
            }
        };
    }
}
