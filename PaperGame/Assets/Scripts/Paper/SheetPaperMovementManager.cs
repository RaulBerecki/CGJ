using UnityEngine;

public class SheetPaperMovementManager : PaperMovementManager
{
    [SerializeField] private float movementMultiplier = 1f; // Base scaling

    public override void MoveBasedOnInput(float movementAmount, float direction)
    {
        targetY = transform.position.y + direction * movementAmount * movementMultiplier;
        isMoving = true;
        currentSpeed = 0f;
    }
}
