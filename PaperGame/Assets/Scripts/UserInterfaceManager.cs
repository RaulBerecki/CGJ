using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel, pausePanel, gameOverPanel, gamePanel, scorePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(false);
        scorePanel.SetActive(false);
        GameObject.Find("Paper").GetComponent<PaperMovementManager>().PauseGravity();
        GameObject.Find("Paper/SheetPaper").GetComponent<PaperWaving>().enabled = false;
    }

    public void GameOn()
    {
        menuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
        scorePanel.SetActive(true);
        GameObject.Find("Paper").GetComponent<PaperMovementManager>().ResumeGravity();
        GameObject.Find("Paper/SheetPaper").GetComponent<PaperWaving>().enabled = true;
    }
    public void GamePause()
    {
        menuPanel.SetActive(false);
        pausePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(false);
        scorePanel.SetActive(true);

        GameObject.Find("Paper").GetComponent<PaperMovementManager>().PauseGravity();
        GameObject.Find("Paper/SheetPaper").GetComponent<PaperWaving>().enabled = false;
    }
    public void GameOver()
    {
        menuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
        scorePanel.SetActive(false);

        GameObject.Find("PlayerController").SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().simulated = false;
        GameObject.Find("Paper").GetComponent<PaperMovementManager>().PauseGravity();
        GameObject.Find("Paper/SheetPaper").GetComponent<PaperWaving>().enabled = false;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
