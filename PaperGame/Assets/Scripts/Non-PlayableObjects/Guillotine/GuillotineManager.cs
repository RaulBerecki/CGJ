using UnityEngine;
using System.Collections;

public class GuillotineManager : MonoBehaviour
{
    private Vector2 initialKnifePosition;
    private Vector2 fallingKnifePosition;

    private float fallSpeed = 10f;
    private float riseSpeed = 2f;

    private float waitTimeAtTop = 1f;
    private float waitTimeAtBottom = 0.5f;

    private void Start()
    {
        initialKnifePosition = gameObject.transform.position;
        fallingKnifePosition = initialKnifePosition - new Vector2(0f, 2.0f);

        StartCoroutine(MoveGuillotineKnife());
    }

    private IEnumerator MoveGuillotineKnife()
    {
        while(true)
        {
            yield return StartCoroutine(MoveToY(fallingKnifePosition.y, fallSpeed));
            yield return new WaitForSeconds(waitTimeAtBottom);

            yield return StartCoroutine(MoveToY(initialKnifePosition.y, riseSpeed));
            yield return new WaitForSeconds(waitTimeAtTop);
        }
    }

    private IEnumerator MoveToY(float y, float speed)
    {
        Vector2 targetPosition = new Vector2(transform.position.x, y);

        while(Mathf.Abs(transform.position.y - y) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.NameToLayer("Paper") == other.gameObject.layer)
        {
            Destroy(other.gameObject.GetComponent<HingeJoint2D>());
        }
    }
}
