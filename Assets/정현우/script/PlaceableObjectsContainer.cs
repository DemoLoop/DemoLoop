using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlaceableObject
{
    public item placedItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;
    public PlaceableObject(item item, Transform target, Vector3Int pos)
    { 
        placedItem = item;  
        targetObject = target;
        positionOnGrid = pos;
    }

}

[CreateAssetMenu(menuName = "Data/Pleaceable Objets Container")]
public class PlaceableObjectsContainer : ScriptableObject
{
    public List<PlaceableObject> placeableObjects;
}
