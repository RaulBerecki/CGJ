using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float score = 0f;
    private int brains = 1;
    private int inkCartridges = 0;

    private TextMeshProUGUI scoreValue = null;
    public bool isPlaying = false;

    void Start()
    {
        scoreValue = GameObject.Find("Canvas/ScorePanel/ScoreValue").GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(incrementScore), 0f, 0.5f);
    }

    private void incrementScore()
    {
        if (isPlaying)
        {
            score += 1.0f;
            scoreValue.text = score.ToString();
        }
    }

    public void incrementBrainValue()
    {
        brains++;
        GameObject.Find("Canvas/ScorePanel/BrainValue").GetComponent<TextMeshProUGUI>().text = brains.ToString();
    }

    public void incrementInkCartridgeValue()
    {
        inkCartridges++;
        GameObject.Find("Canvas/ScorePanel/InkCartridgeValue").GetComponent<TextMeshProUGUI>().text = inkCartridges.ToString();
    }

    public int getBrainValue()
    {
        return brains;
    }

    public int getInkCartridgeValue()
    {
        return inkCartridges;
    }

    public void decrementBrainValue()
    {
        brains--;
        GameObject.Find("Canvas/ScorePanel/BrainValue").GetComponent<TextMeshProUGUI>().text = brains.ToString();
    }

    public void decrementInkCartridgeValue()
    {
        inkCartridges--;
        GameObject.Find("Canvas/ScorePanel/InkCartridgeValue").GetComponent<TextMeshProUGUI>().text = inkCartridges.ToString();
    }
}