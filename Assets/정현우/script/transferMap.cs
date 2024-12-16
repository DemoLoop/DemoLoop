// 작성자 : 정현우, 날짜 : 2024-09-06 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transferMap1 : MonoBehaviour
{
    public Transform target;
    public GameObject thePlayer;
    private CameraManager theCamera;
    public BoxCollider2D targetBound;
    private FadeManager theFade;// 페이드 아웃 관련
    //private OrderManager theOrder;

    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name == "Player"){
            StartCoroutine(TransferCoroutine());
        }
        
    }

    IEnumerator TransferCoroutine() {
        //theOrder.PreLoadCharacteer();
        //theOrder.NotMove();
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        //theCamera.SetBound(targetBound); // 카메라의 바운드를 변경
        thePlayer.transform.position = target.transform.position;
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        theFade.FadeIn();
        //theOrder.Move();
    }
}
