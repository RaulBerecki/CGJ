using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PaperWaving : MonoBehaviour
{
    private List<Transform> paperSegments = new List<Transform>(); // Segments from top to bottom or left to right
    private float waveSpeed = 3f;
    private float waveAmplitude = 5f;
    private float segmentOffset = 0.025f;

    private float verticalAmplitude = 0.125f; // How much it moves up and down
    private List<Vector3> initialPositions = new List<Vector3>();

    private float targetHeight;
    private int hitIndex;
    private bool isWaving = false;
    void Start()
    {
        foreach(Transform child in gameObject.transform)
        {
            if(child.gameObject.name.Contains("PaperBit"))
            {
                paperSegments.Add(child);
                initialPositions.Add(child.localPosition);
            }
        }
    }
    void Update()
    {
        //if(!isWaving)
        //{
        //    StartCoroutine(ripplePaper(hitIndex, hitIndex));
        //    return;
        //}

        //for (int i = 0; i < paperSegments.Count; i++)
        //{
        //    float offset = i * segmentOffset;
        //    float wave = Mathf.Sin(Time.time * waveSpeed + offset);

        //    //Rotation Movement
        //    float angle = Mathf.Sin(Time.time * waveSpeed + offset) * waveAmplitude;
        //    paperSegments[i].localRotation = Quaternion.Euler(0, 0, angle);

        //    // Vertical Movement
        //    Vector3 pos = initialPositions[i];
        //    pos.y += wave * verticalAmplitude;
        //    paperSegments[i].localPosition = pos;
        //}
    }

    public void pushPaper(Transform hitBit)
    {
        isWaving = false;

        hitIndex = paperSegments.IndexOf(hitBit);
        targetHeight = paperSegments[hitIndex].gameObject.transform.position.y + 0.2f;        
    }

    IEnumerator<WaitForSeconds> ripplePaper(int hitIndex, int currentIndex, bool goLeft = true, bool goRight = true)
    {
        if(currentIndex < 0 || currentIndex >= paperSegments.Count)
        {
            yield break;
        }
        paperSegments[currentIndex].gameObject.transform.position = Vector2.MoveTowards(
            paperSegments[currentIndex].gameObject.transform.position,
            new Vector2(
                paperSegments[currentIndex].gameObject.transform.position.x,
                targetHeight
                ),
            0.10f// * getNormalizedPercentage(currentIndex, 0, paperSegments.Count, hitIndex) * Time.deltaTime
            );

        yield return new WaitForSeconds(0.02f);
        if (goLeft)
        {
            StartCoroutine(ripplePaper(hitIndex, currentIndex - 1, true, false));
        }
        if(goRight)
        {
            StartCoroutine(ripplePaper(hitIndex, currentIndex + 1, false, true));
        }
    }

    private float getNormalizedPercentage(int elementIndex, int leftLimitIndex, int rightLimitIndex, int middleIndex)
    {
        float r = (rightLimitIndex - leftLimitIndex) / (2f * Mathf.Sqrt(Mathf.Log(10f)));
        return 0.1f + 0.9f * Mathf.Exp(-Mathf.Pow((elementIndex - middleIndex) / r, 2f));
    }
}
