using UnityEngine;

public class SimpleGroundSetup : MonoBehaviour
{
    [Header("Ground Generation")]
    public Material groundMaterial;
    public int groundSize = 50;

    void Start()
    {
        CreateGround();
    }

    void CreateGround()
    {
        // Create ground plane
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(groundSize, 1, groundSize);
        ground.transform.position = Vector3.zero;

        if (groundMaterial != null)
            ground.GetComponent<Renderer>().material = groundMaterial;
    }
}