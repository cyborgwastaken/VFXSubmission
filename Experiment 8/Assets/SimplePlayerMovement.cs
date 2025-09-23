using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [Tooltip("The speed at which the object moves.")]
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // 1. GET INPUT
        // Input.GetAxisRaw provides immediate, non-smoothed input.
        // "Horizontal" is mapped to A/D and Left/Right arrows.
        // "Vertical" is mapped to W/S and Up/Down arrows.
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 2. CREATE MOVEMENT VECTOR
        // Create a direction vector from the input.
        // .normalized ensures that diagonal movement isn't faster.
        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;

        // 3. APPLY MOVEMENT
        // Move the object's transform based on the direction, speed,
        // and time passed since the last frame (Time.deltaTime).
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}