using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    int swipes = 0;
    //DoubleClickVars
    public float doubleClickThreshold = 0.3f; // Time window for double click
    private float lastClickTime = 0f;

    [SerializeField] private RoomManager roomManager;
    [SerializeField] private UserInterfaceManager userInterfaceManager;
    [SerializeField] private InkManager inkManager;
    void Update()
    {
        CheckForDoubleClick();
        if (Input.GetMouseButtonDown(0))
        {
            OnClickStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (swipes == 0)
            {
                if(null != roomManager)
                {
                    roomManager.isPlaying = true;
                }
                if(null != inkManager)
                {
                    inkManager.isPlaying = true;
                }
                if(null != userInterfaceManager)
                {
                    userInterfaceManager.GameOn();
                }
                swipes++;
            }
            OnClickEnd();
        }
    }

    private void CheckForDoubleClick()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            if (Time.time - lastClickTime < doubleClickThreshold)
            {
                // Double click detected
                if (roomManager.isPlaying)
                {
                    roomManager.isPlaying = false;
                    inkManager.isPlaying = false;
                    userInterfaceManager.GamePause();
                }
                else
                {
                    roomManager.isPlaying = true;
                    inkManager.isPlaying = true;
                    userInterfaceManager.GameOn();
                }
            }
            lastClickTime = Time.time;
        }
    }
    private void OnClickStart()
    {
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnClickEnd()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        applyForce();
    }

    private void applyForce()
    {
        Vector2 linearVelocity = (endPoint - startPoint) * 5.0f;

        if (Mathf.Abs(linearVelocity.y) < 0.1f)
            return;

        float movementAmount = linearVelocity.magnitude * 0.05f;
        float direction = Mathf.Sign(linearVelocity.y);

        GameObject paper = GameObject.Find("Paper");
        PaperMovementManager movementManager = paper.GetComponent<PaperMovementManager>();

        movementManager.MoveBasedOnInput(movementAmount, direction);
    }

    public void changePlaneMode()
    {
        GameObject paper = GameObject.Find("Paper");
        PaperMovementManager movementManager = paper.GetComponent<PaperMovementManager>();

        if(movementManager is SheetPaperMovementManager)
        {
            paper.AddComponent<PlanePaperMovementManager>();
            paper.transform.Find("PlanePaper").gameObject.SetActive(true);
            paper.transform.Find("SheetPaper").gameObject.SetActive(false);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/PlaneOutline").SetActive(false);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/SheetOutline").SetActive(true);
        }
        else if(movementManager is PlanePaperMovementManager)
        {
            paper.AddComponent<SheetPaperMovementManager>();
            paper.transform.Find("PlanePaper").gameObject.SetActive(false);
            paper.transform.Find("SheetPaper").gameObject.SetActive(true);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/PlaneOutline").SetActive(true);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/SheetOutline").SetActive(false);
        }

        Destroy(movementManager);
    }
}
