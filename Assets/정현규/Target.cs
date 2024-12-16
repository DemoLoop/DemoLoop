using UnityEngine;

public class Target : MonoBehaviour
{
    public void Update()
    {
        GameObject astarObject = GameObject.Find("AMANAGER");

        if (astarObject == null)
        {
            Debug.LogError("GameObject 'AMANAGER'를 찾을 수 없습니다. Hierarchy에서 확인하세요.");
            return;
        }

        PathFinding pathFinding = astarObject.GetComponent<PathFinding>();

        if (pathFinding == null)
        {
            Debug.LogError("'AMANAGER' 오브젝트에 PathFinding 컴포넌트가 없습니다. Inspector에서 확인하세요.");
            return;
        }

        pathFinding.touchOrigin = (Vector2)transform.position;
    }
}
