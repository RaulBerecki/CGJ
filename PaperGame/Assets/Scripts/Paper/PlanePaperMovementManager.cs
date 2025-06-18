using UnityEngine;

public class PlanePaperMovementManager : PaperMovementManager
{
    [SerializeField] private float movementMultiplier = 1.5f; // More responsive

    public override void MoveBasedOnInput(float movementAmount, float direction)
    {
        targetY = transform.position.y + direction * movementAmount * movementMultiplier;
        isMoving = true;
        currentSpeed = 0f;
    }
}
