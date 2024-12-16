using UnityEngine;

public class Target : MonoBehaviour
{
    public void Update()
    {
        GameObject astarObject = GameObject.Find("AMANAGER");

        if (astarObject == null)
        {
            Debug.LogError("GameObject 'AMANAGER'�� ã�� �� �����ϴ�. Hierarchy���� Ȯ���ϼ���.");
            return;
        }

        PathFinding pathFinding = astarObject.GetComponent<PathFinding>();

        if (pathFinding == null)
        {
            Debug.LogError("'AMANAGER' ������Ʈ�� PathFinding ������Ʈ�� �����ϴ�. Inspector���� Ȯ���ϼ���.");
            return;
        }

        pathFinding.touchOrigin = (Vector2)transform.position;
    }
}
