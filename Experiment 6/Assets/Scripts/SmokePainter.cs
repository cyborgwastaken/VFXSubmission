using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SmokePainter : MonoBehaviour
{
    [Header("Smoke Prefab")]
    public GameObject smokePrefab;

    [Header("Paint Settings")]
    public float brushSize = 3f;
    public LayerMask groundLayer = 1;

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, brushSize);
    }

    public void PlaceSmoke()
    {
        if (smokePrefab == null) return;

        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundLayer))
        {
            GameObject smoke = Instantiate(smokePrefab, hit.point, Quaternion.identity, transform);
            smoke.transform.up = hit.normal;
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(SmokePainter))]
public class SmokePainterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SmokePainter painter = (SmokePainter)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Place Smoke"))
        {
            painter.PlaceSmoke();
        }
    }

    void OnSceneGUI()
    {
        SmokePainter painter = (SmokePainter)target;

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && e.control)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                painter.transform.position = hit.point;
                painter.PlaceSmoke();
                e.Use();
            }
        }
    }
}
#endif