using UnityEngine;

public class PaperCreator : MonoBehaviour
{
    private Rigidbody2D lastRigidbody2D = null;

    private void Awake()
    {
        int paperLength = 40;
        GameObject paperBit = Resources.Load<GameObject>("PaperBit");

        for(int iterator=0 ; iterator<paperLength ; iterator++)
        {
            GameObject currentPaperBit = GameObject.Instantiate(paperBit, gameObject.transform.Find("SheetPaper"));

            currentPaperBit.AddComponent<HingeJoint2D>();
            HingeJoint2D hj = currentPaperBit.GetComponent<HingeJoint2D>();
            hj.connectedBody = lastRigidbody2D;
            if(null == lastRigidbody2D)
            {
                hj.useConnectedAnchor = false;
            }

            lastRigidbody2D = currentPaperBit.GetComponent<Rigidbody2D>();
            lastRigidbody2D.gravityScale = 0f;
            lastRigidbody2D.freezeRotation = true;

            currentPaperBit.transform.position += new Vector3(0.025f * iterator, 0f);
        }
    }
}
