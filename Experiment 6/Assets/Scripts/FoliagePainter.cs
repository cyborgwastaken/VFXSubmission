using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FoliagePainter : MonoBehaviour
{
    [Header("Foliage Settings")]
    public GameObject[] foliagePrefabs;
    public float brushSize = 5f;
    public int spawnCount = 10;
    public float minScale = 0.8f;
    public float maxScale = 1.2f;
    public LayerMask groundLayer = 1;

    [Header("Randomization")]
    public bool randomRotation = true;
    public bool randomScale = true;
    public float slopeLimit = 45f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, brushSize);
    }

    public void PaintFoliage()
    {
        if (foliagePrefabs.Length == 0) return;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPoint = GetRandomPointInCircle();

            if (Physics.Raycast(randomPoint + Vector3.up * 100, Vector3.down, out RaycastHit hit, 200f, groundLayer))
            {
                // Check slope
                float slope = Vector3.Angle(hit.normal, Vector3.up);
                if (slope > slopeLimit) continue;

                // Select random prefab
                GameObject prefab = foliagePrefabs[Random.Range(0, foliagePrefabs.Length)];

                // Instantiate
                GameObject instance = Instantiate(prefab, hit.point, Quaternion.identity, transform);

                // Apply random rotation
                if (randomRotation)
                {
                    instance.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                }

                // Apply random scale
                if (randomScale)
                {
                    float scale = Random.Range(minScale, maxScale);
                    instance.transform.localScale = Vector3.one * scale;
                }

                // Align to surface normal
                instance.transform.up = hit.normal;

                spawnedObjects.Add(instance);
            }
        }
    }

    public void ClearFoliage()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] != null)
                DestroyImmediate(spawnedObjects[i]);
        }
        spawnedObjects.Clear();
    }

    Vector3 GetRandomPointInCircle()
    {
        Vector2 randomCircle = Random.insideUnitCircle * brushSize;
        return transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(FoliagePainter))]
public class FoliagePainterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FoliagePainter painter = (FoliagePainter)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Paint Foliage"))
        {
            painter.PaintFoliage();
        }

        if (GUILayout.Button("Clear Foliage"))
        {
            painter.ClearFoliage();
        }
    }

    void OnSceneGUI()
    {
        FoliagePainter painter = (FoliagePainter)target;

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && e.shift)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                painter.transform.position = hit.point;
                painter.PaintFoliage();
                e.Use();
            }
        }
    }
}
#endif