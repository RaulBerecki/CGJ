using UnityEngine;

public class BrainController : MonoBehaviour
{
    public BrainManager brainManager;

    private void Start()
    {
        brainManager = GameObject.Find("BrainManager").GetComponent<BrainManager>();
    }

    private void Update()
    {
        if (-3.0f > gameObject.transform.position.x)
        {
            GameObject.Destroy(gameObject);
        }
        if (brainManager.isPlaying)
            gameObject.transform.position = Vector2.MoveTowards(
                gameObject.transform.position,
                gameObject.transform.position + new Vector3(-1.0f, 0f, 0f),
                0.01f
                );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.NameToLayer("Paper") == other.gameObject.layer)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
