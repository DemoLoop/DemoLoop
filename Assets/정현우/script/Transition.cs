// 작성자 : 정현우
// 스크립트 일부 수정(기존 스크립트는 임시로 주석처리) 

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public enum TrasitionType
{ 
    Warp,
    Scence
}

public class Transition : MonoBehaviour
{
    [SerializeField] TrasitionType transitionType;
    [SerializeField] string sceneNameToTransition;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] PlayerManager playerManager;

    Transform destination;

    void Start()
    {
        //  transform.GetChild(1)은 현재 스크립트가 붙어 있는 게임 오브젝트의
        //  두 번째 자식(인덱스는 0부터 시작하므로 1은 두 번째 자식)을 가져오는 것입니다.
        //  즉, destination 변수는 현재 게임 오브젝트의 두 번째 자식의 Transform을 참조
        
        destination = transform.GetChild(1); // Destination 객체를 가져옴
        playerManager = FindObjectOfType<PlayerManager>();
    }

    internal void InitiateTransition(Transform toTransition)
    {

        //Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();

        switch (transitionType) 
        {
            case TrasitionType.Warp:  // 일반적으로 빠른 이동 또는 순간 이동
                
                //currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, destination.position - toTransition.position);
                
                toTransition.position = new Vector3(destination.position.x, destination.position.y, toTransition.position.z);
                break;

            case TrasitionType.Scence: // Scene은 유니티에서 다른 씬으로 이동하는 것

                //currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(toTransition, targetPosition - toTransition.position);
                
                GameScenceManager.instance.InitSwitchScene(sceneNameToTransition, targetPosition);
                break;
        }
 
    }
}
