using UnityEngine;


public class Path : MonoBehaviour
{
    private int index;

    public Vector3 GetNextPoint()
    {
        index = index + 1 == transform.childCount ? 0 : index + 1;
        return transform.GetChild(index).position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var nextChild = transform.GetChild(i + 1 == transform.childCount ? 0 : i + 1);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(child.position, nextChild.position);
            Gizmos.DrawSphere(child.position, 1f);
        }
    }
#endif

}
