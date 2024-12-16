// 작성자 : 정현우, 현재 버그가 있는 코드라서 임시처리 했음, Sprite plowed;  임시코드


using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[Serializable]
public class CropTile
{
    Sprite plowed; // 임시 해결 코드 11-06

    // 작물의 성장 시간(얼마나 시간이 지났는지)
    public int growTimer;

    // 작물의 성장 단계(몇 단계까지 자랐는지)
    public int growStage;

    // 심어진 작물 정보
    public Crop crop;

    // 작물의 모습을 보여줄 SpriteRenderer (이미지 렌더링)
    public SpriteRenderer renderer; // 현재 여기서 버그 발생


    // 작물이 받은 피해량 (얼마나 손상되었는지)
    public float damage;

    // 타일의 위치 정보 (타일 좌표)
    public Vector3Int position;

    // 작물이 완전히 자랐는지 확인하는 속성
    public bool Complete
    {
        get
        {
            // 만약 crop이 null(심어지지 않은 상태)라면 false 반환
            if (crop == null) { return false; }
            // growTimer가 crop의 성장 시간을 넘었는지 확인하여 반환
            return growTimer >= crop.TimeToGrow;
        }
    }

    // 작물이 수확되었을 때 실행되는 함수
    internal void Harvested()
    {
        // 모든 상태를 초기화
        growTimer = 0;
        growStage = 0;
        crop = null;  // 작물이 없어진다.

        // renderer.gameObject.SetActive(false);  // 화면에서 안 보이게 설정 // 임시조치 11-06, 잠시 주석처리

        renderer.sprite = plowed; // 임시조치 11-06  renderer.gameObject.SetActive(false); 이거 대체함

        damage = 0;  // 피해량 초기화
    }
}


public class CropsManager : MonoBehaviour
{
    public TilemapCropsManager cropsManager;

    public void PickUp(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return; 
        }

        cropsManager.PickUp(position);

    }

    public bool Check(Vector3Int position)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return false;
        }

        return cropsManager.Check(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }

        cropsManager.Seed(position, toSeed);
    }

    public void Plow(Vector3Int position) 
    {
        if (cropsManager == null)
        {
            Debug.LogWarning("No tilemap crops manager are referenced in the crops manager");
            return;
        }

        cropsManager.Plow(position);
    } 
}