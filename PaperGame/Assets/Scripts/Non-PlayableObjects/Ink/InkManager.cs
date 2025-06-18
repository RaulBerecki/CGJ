using UnityEngine;

public class InkManager : MonoBehaviour
{
    private GameObject inkCartridgePrefab;
    public bool isPlaying;

    private void Awake()
    {
        inkCartridgePrefab = Resources.Load<GameObject>("InkCartridge");
    }

    private void Start()
    {
        isPlaying = false;
        InvokeRepeating(nameof(SpawnInkCartridge), 1.0f, 2.0f);
    }

    private void SpawnInkCartridge()
    {
        if(isPlaying)
        {
            float height = Random.Range(-2.0f, 2.5f);
            GameObject inkCartridge = GameObject.Instantiate(inkCartridgePrefab, new Vector2(5.0f, height), Quaternion.identity);
            inkCartridge.GetComponent<InkCartridgeController>().inkManager=this;
        }
    }
}
