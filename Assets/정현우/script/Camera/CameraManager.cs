// 작성자 : 정현우, 날짜 : 2024-09-06 

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static CameraManager instance;

    public GameObject target; // 카메라가 따라가는 대상 ex) player

    public BoxCollider2D bound; // 카메라 영역 제한을 위한 박스콜라이더

    private Vector3 minBound; // 박스 콜라이더 영역의 최댓값을 지님
    private Vector3 maxBound; // 박스 콜라이더 영역의 최소값을 지님

    private float halfWidth; // 카메라의 절반 가로(카메라가 맵 밖으로 삐져 나가는 걸 막음)
    private float halfHeight; // 카메라의 절반 높이(카메라가 맵 밖으로 삐져 나가는 걸 막음)

    private Camera theCamera; // 이건 카메라의 반높이 값을 구하기 위한 변수, Camera의 속성을 활용

    private Collider Boxcollider;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private Vector3 targetPosition;

    void Start()
    {
        Boxcollider = FindObjectOfType<Collider>();
        bound = Boxcollider.GetComponent<BoxCollider2D>(); 

        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize; // 카메라의 사이즈(size) 가져옴 
        halfWidth = halfHeight * Screen.width / Screen.height; // 반너비를 구하는 일종의 공식
        // Screen.width / Screen.height << 게임 해상도 나타냄, 
    }

    void LateUpdate()
    {
        if (target.gameObject != null)
        {
            this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);


            // Clamp 함수는 3개의 인수를 갖음 (값, 최소값, 최대값)
            // (10, 0, 100)
            // 값이 10이면 리턴값은 10이다. 이유는 10은 0~100 사이에 있기 때문이다
            // (-100, 0, 100)
            // 값이 -100이면 리턴값은 0이 된다. 이유는 10은 0~100 사이를 벗어났기 때문이다
            // (1000, 0, 100)
            // 값이 1000이면 리턴값은 100이 된다. 이유는 1000은 0~100 사이를 벗어났기 때문이다

            // Clamp 함수를 활용하여, 카메라 영역 지정
            // 최솟값은 반높이, 반너비를 더하고, 최댓값은 반높이, 반너비 뺀다
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            //카메라 위치 지정
            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }

}
