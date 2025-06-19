using UnityEngine;

public class SheetPaperMovementManager : PaperMovementManager
{
    [SerializeField] private float movementMultiplier = 0.5f; // Base scaling

    private void Start()
    {
        maxTiltAngle = 5f;
    }

    public override void MoveBasedOnInput(float movementAmount, float direction)
    {
        targetY = transform.position.y + direction * movementAmount * movementMultiplier;
        isMoving = true;
        currentSpeed = 0f;
    }
}
