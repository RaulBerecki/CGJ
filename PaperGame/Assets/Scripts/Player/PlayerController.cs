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
    [SerializeField] private BrainManager brainManager;

    //Audios
    bool paperPlaying, windPlaying;
    [SerializeField] private AudioSource paperAudio, windAudio;
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
                if(null != brainManager)
                {
                    brainManager.isPlaying = true;
                }
                if(null != userInterfaceManager)
                {
                    userInterfaceManager.GameOn();
                }
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().isPlaying = true;
                swipes++;
            }
            windPlaying = true;
            OnClickEnd();
        }
        AudioPlaying();
    }

    private void AudioPlaying()
    {
        if(windPlaying) { 
            if(paperPlaying)
            {
                if(0 <= GameObject.Find("ScoreManager").GetComponent<ScoreManager>().getBrainValue())
                {
                    paperAudio.pitch = Random.Range(0.8f, 1.2f);
                    paperAudio.Play();
                }    
            }
            else
            {
                windAudio.pitch = Random.Range(0.8f, 1.2f);
                windAudio.Play();
            }
        }
        windPlaying=false;
        paperPlaying=false;
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
                    brainManager.isPlaying = false;
                    userInterfaceManager.GamePause();
                    GameObject.Find("ScoreManager").GetComponent<ScoreManager>().isPlaying = false;
                }
                else
                {
                    roomManager.isPlaying = true;
                    inkManager.isPlaying = true;
                    brainManager.isPlaying = true;
                    userInterfaceManager.GameOn();
                    GameObject.Find("ScoreManager").GetComponent<ScoreManager>().isPlaying = true;
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
        if(roomManager.isPlaying )
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
        paperPlaying = true;
        GameObject paper = GameObject.Find("Paper");
        PaperMovementManager movementManager = paper.GetComponent<PaperMovementManager>();

        if(0 < GameObject.Find("ScoreManager").GetComponent<ScoreManager>().getBrainValue() && movementManager is SheetPaperMovementManager)
        {
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().decrementBrainValue();

            paper.AddComponent<PlanePaperMovementManager>();
            paper.transform.Find("PlanePaper").gameObject.SetActive(true);
            paper.transform.Find("SheetPaper").gameObject.SetActive(false);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/PlaneOutline").SetActive(false);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/SheetOutline").SetActive(true);
            Destroy(movementManager);
        }
        else if(movementManager is PlanePaperMovementManager)
        {
            paper.AddComponent<SheetPaperMovementManager>();
            paper.transform.Find("PlanePaper").gameObject.SetActive(false);
            paper.transform.Find("SheetPaper").gameObject.SetActive(true);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/PlaneOutline").SetActive(true);
            GameObject.Find("Canvas/GamePanel/PaperModeButton/SheetOutline").SetActive(false);
            Destroy(movementManager);
        }
    }
}
