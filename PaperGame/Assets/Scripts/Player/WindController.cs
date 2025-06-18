using UnityEngine;

public class WindController : MonoBehaviour
{
    //private float detectionDistanceBuffer = 0.05f;
    //private Rigidbody2D rb = null;

    //private void Start()
    //{
    //    rb = gameObject.GetComponent<Rigidbody2D>();
    //}

    //private void FixedUpdate()
    //{
    //    Vector2 velocity = rb.linearVelocity;

    //    if(velocity != Vector2.zero)
    //    {
    //        Vector2 direction = velocity.normalized;
    //        float maxFrameTravel = velocity.magnitude * Time.deltaTime;
    //        float rayLength = maxFrameTravel + detectionDistanceBuffer;

    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, LayerMask.GetMask("Paper"));

    //        if(hit.collider != null)
    //        {
    //            Debug.Log("Hit paper layer: " + hit.collider.name);
    //            rb.linearVelocity = Vector2.zero;

    //            Vector2 moveDirection = Vector2.zero;
    //            //if(velocity.y > 0.1f)
    //            //{
    //            //    moveDirection = Vector2.up;
    //            //}
    //            //else if(velocity.y < -0.1f)
    //            //{
    //            //    moveDirection = Vector2.down;
    //            //}

    //            //float movementAmount = velocity.magnitude * 0.05f; //scale factor
    //            //hit.collider.transform.parent.gameObject.GetComponent<PaperMovementManager>().setTargetLocationY(
    //            //    hit.collider.transform.position.y + (moveDirection.y * movementAmount)
    //            //    );

    //            GameObject.Destroy(gameObject);
    //        }
    //    }
    //}
}
