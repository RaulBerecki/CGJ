using UnityEngine;

public class ShredderController : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        // Rotate around Z axis (for 2D)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
