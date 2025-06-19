using UnityEngine;

public class CheckVisibility : MonoBehaviour
{
    public bool isVisible;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameVisible()
    {
        isVisible=true;
    }
}
