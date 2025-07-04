using UnityEngine;

public abstract class PaperMovementManager : MonoBehaviour
{
    protected float targetY;
    protected bool isMoving = false,waterTouch=false;
    protected float currentSpeed = 0f;

    public float maxSpeed = 5f;
    public float acceleration = 10f;
    public float decelerationDistance = 0.5f;

    protected float maxTiltAngle;
    private float tiltSpeed = 5f;
    private bool useGravity = true;

    private Vector2 externalForce = Vector2.zero;

    public SpriteRenderer sprite;
    protected virtual void Start()
    {
        sprite=gameObject.transform.Find("PlanePaper").GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (Mathf.Abs(0f - gameObject.transform.position.x) < 0.1f)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityX = 0f;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocityX = 1.0f * Mathf.Sign(0f - gameObject.transform.position.x);
        }

        if (isMoving)
        {
            float distance = Mathf.Abs(targetY - transform.position.y);
            float direction = Mathf.Sign(targetY - transform.position.y);

            float speedLimit = (distance < decelerationDistance)
                ? Mathf.Lerp(0, maxSpeed, distance / decelerationDistance)
                : maxSpeed;

            float previousSpeed = currentSpeed;
            currentSpeed = Mathf.MoveTowards(currentSpeed, speedLimit, acceleration * Time.deltaTime);
            float accelerationDelta = (currentSpeed - previousSpeed) / Time.deltaTime;

            transform.Translate(Vector2.up * direction * currentSpeed * Time.deltaTime);

            // --- Tilt Calculation ---
            float tiltAngle = -Mathf.Clamp(accelerationDelta, -acceleration, acceleration) / acceleration * maxTiltAngle * -direction;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, tiltAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);

            if (distance < 0.01f)
            {
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                isMoving = false;
                currentSpeed = 0f;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * tiltSpeed);

            gameObject.GetComponent<Rigidbody2D>().linearVelocityY = 0f;
            float gentleFallSpeed = waterTouch ? 2.0f : 0.5f;

            if(useGravity)
            {
                transform.Translate(Vector2.down * gentleFallSpeed * Time.deltaTime);
            }
        }

        if (externalForce != Vector2.zero)
        {
            transform.Translate(externalForce * Time.deltaTime);
        }
    }

    public void PauseGravity()
    {
        useGravity = false;
    }

    public void ResumeGravity()
    {
        useGravity = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            waterTouch = true;
            sprite.color = new Color32(175, 175, 175, 175);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fanTrigger"))
        {
            Vector2 direction = (collision.bounds.center - transform.position).normalized;
            externalForce = -direction * 1.45f;
        }

        if(collision.gameObject.CompareTag("hood"))
        {
            externalForce = Vector2.up * 2.0f;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fanTrigger") || collision.gameObject.CompareTag("hood"))
        {
            externalForce = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (LayerMask.NameToLayer("Shredder") == other.collider.gameObject.layer)
        {
            GameObject.Find("UserInterfaceManager").GetComponent<UserInterfaceManager>().GameOver();
            return;
        }

        foreach (ContactPoint2D contact in other.contacts)
        {
            Vector2 contactNormal = contact.normal.normalized;

            if (Mathf.Abs(contactNormal.y) > Mathf.Abs(contactNormal.x))
            {
                targetY = transform.position.y;
                isMoving = false;
                currentSpeed = 0f;
                break;
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().linearVelocityX = 1.0f;
    }

    public abstract void MoveBasedOnInput(float movementAmount, float direction);
}