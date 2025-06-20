using UnityEngine;

public class BrainManager : MonoBehaviour
{
    private GameObject brainPrefab;
    public bool isPlaying;

    private void Awake()
    {
        brainPrefab = Resources.Load<GameObject>("Brain");
    }

    private void Start()
    {
        isPlaying = false;
        InvokeRepeating(nameof(SpawnBrain), 1.2f, 3.8f);
    }

    private void SpawnBrain()
    {
        if (isPlaying)
        {
            float height = Random.Range(-3.0f, 3.5f);
            GameObject inkCartridge = GameObject.Instantiate(brainPrefab, new Vector2(5.0f, height), Quaternion.identity);
            inkCartridge.GetComponent<BrainController>().brainManager = this;
        }
    }
}
