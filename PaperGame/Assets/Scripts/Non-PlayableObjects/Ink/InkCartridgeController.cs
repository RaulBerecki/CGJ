using UnityEngine;

public class InkCartridgeController : MonoBehaviour
{
    public InkManager inkManager;
    private void Update()
    {
        if(-3.0f > gameObject.transform.position.x)
        {
            GameObject.Destroy(gameObject);
        }
        if(inkManager.isPlaying)
            gameObject.transform.position = Vector2.MoveTowards(
                gameObject.transform.position,
                gameObject.transform.position + new Vector3(-1.0f, 0f, 0f),
                0.01f
                );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(LayerMask.NameToLayer("Paper") == other.gameObject.layer)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
