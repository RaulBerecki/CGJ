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
    }

    public void GameOn()
    {
        menuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
        scorePanel.SetActive(true);
    }
    public void GamePause()
    {
        menuPanel.SetActive(false);
        pausePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(false);
        scorePanel.SetActive(true);
    }
    public void GameOver()
    {
        menuPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
        scorePanel.SetActive(false);

        GameObject.Find("PlayerController").SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
