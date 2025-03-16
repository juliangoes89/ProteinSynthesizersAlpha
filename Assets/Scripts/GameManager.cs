using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
	public GameObject gameWinCanvas;
    public Button restartButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		gameWinCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
		restartButton.onClick.AddListener(RestartGame);
	}

    public void GameOver()
	{
		Time.timeScale = 0f;
		gameOverCanvas.SetActive(true);
	}
	public void GameWin() {
		Time.timeScale = 0f;
		gameWinCanvas.SetActive(true);
	}
    public void RestartGame(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
