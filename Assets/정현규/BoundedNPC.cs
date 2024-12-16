using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : MovingObject
{
    // NPC 매니저로 상속
    int direction = Random.Range(0, 4);
}
