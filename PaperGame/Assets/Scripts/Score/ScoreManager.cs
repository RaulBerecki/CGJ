using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float score = 0f;
    private TextMeshProUGUI scoreValue = null;
    public bool isPlaying = false;

    void Start()
    {
        scoreValue = GameObject.Find("Canvas/ScorePanel/ScoreValue").GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(incrementScore), 0f, 0.5f);
    }

    private void incrementScore()
    {
        if(isPlaying)
        {
            score += 1.0f;
            scoreValue.text = score.ToString();
        }
    }
}
