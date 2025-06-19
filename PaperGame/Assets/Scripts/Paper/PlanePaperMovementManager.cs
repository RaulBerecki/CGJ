using UnityEngine;

public class PlanePaperMovementManager : PaperMovementManager
{
    [SerializeField] private float movementMultiplier = 1.5f; // More responsive

    private void Start()
    {
        maxTiltAngle = 15f;
    }

    public override void MoveBasedOnInput(float movementAmount, float direction)
    {
        targetY = transform.position.y + direction * movementAmount * movementMultiplier;
        isMoving = true;
        currentSpeed = 0f;
    }
}
