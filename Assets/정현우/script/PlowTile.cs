//2024-10-03, 작성자 : 정현우
//이 코드는 Unity의 Tilemap 시스템을 사용하여 특정 타일을 경작하는 도구 행동을 정의하는 스크립트입니다.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
    // canPlow라는 리스트를 사용하여 경작할 수 있는 타일 타입을 정의
    [SerializeField] List<TileBase> canPlow;
    [SerializeField] AudioClip onPlowUsed;
    
    public override bool OnApplyToTileMap(Vector3Int gridPostion, TileMapReadController tileMapReadController, item item)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPostion);

        if(canPlow.Contains(tileToPlow) == false)
        {
            // canPlow 리스트에 포함되어 있지 않으면 false를 반환하여 경작이 불가능함
            return false;
        }

        //타일이 경작 가능한 경우, cropsManager를 통해 경작 작업을 수행하고 true
        tileMapReadController.cropsManager.Plow(gridPostion);

        AudioManager.instance.Play(onPlowUsed);
        return true;
    }
}
