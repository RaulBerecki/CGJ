using UnityEngine;

public class PaperCreator : MonoBehaviour
{
    private void Awake()
    {
        int paperLength = 40;
        GameObject paperBit = Resources.Load<GameObject>("PaperBit");

        for(int iterator=0 ; iterator<paperLength ; iterator++)
        {
            GameObject currentPaperBit = GameObject.Instantiate(paperBit, gameObject.transform.Find("SheetPaper"));

            currentPaperBit.transform.position += new Vector3(0.025f * iterator, 0f);
        }
    }
}
