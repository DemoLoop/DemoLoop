// 2024-10-27, 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 클래스는 농작물 데이터를 저장하는 역할

[CreateAssetMenu(menuName = "Data/Crops Container")]
public class CropsContainer : ScriptableObject
{

    // CropTile은 농작물의 정보를 담고 있는 클래스
    public List<CropTile> crops;

    public CropTile Get(Vector3Int position)
    {
        // crops 리스트에서 position과 일치하는 농작물을 찾아 반환합니다. 만약 일치하는 농작물이 없다면 null을 반환
        // x는 crops 리스트의 각 농작물 객체를 가리키며,
        // Find 메서드는 position 속성이 주어진 position과 일치하는 첫 번째 농작물 객체를 반환
        return crops.Find(x => x.position == position);
    }

    public void Add(CropTile crop)
    { 
        crops.Add(crop);
    }
}
