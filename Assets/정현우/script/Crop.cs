// 2024-10-09 작성자 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Data/Crop")]
public class Crop : ScriptableObject
{
    public TileBase plowed;
    public int TimeToGrow = 10;
    public item yield;
    public int count = 1;

    public List<Sprite> sprites;
    public List<int> growthStageTime;

}
