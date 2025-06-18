using UnityEngine;

public abstract class PaperMovementManager : MonoBehaviour
{
    protected float targetY;
    protected bool isMoving = false;
    protected float currentSpeed = 0f;

    public float maxSpeed = 5f;
    public float acceleration = 10f;
    public float decelerationDistance = 0.5f;

    protected virtual void Update()
    {
        if (isMoving)
        {
            float distance = Mathf.Abs(targetY - transform.position.y);
            float direction = Mathf.Sign(targetY - transform.position.y);

            float speedLimit = (distance < decelerationDistance)
                ? Mathf.Lerp(0, maxSpeed, distance / decelerationDistance)
                : maxSpeed;

            currentSpeed = Mathf.MoveTowards(currentSpeed, speedLimit, acceleration * Time.deltaTime);
            transform.Translate(Vector2.up * direction * currentSpeed * Time.deltaTime);

            if (distance < 0.01f)
            {
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                isMoving = false;
                currentSpeed = 0f;
            }
        }
    }

    // Abstract method to define how movement amount is scaled
    public abstract void MoveBasedOnInput(float movementAmount, float direction);
}
