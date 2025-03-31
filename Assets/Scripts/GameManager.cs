using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject gameOverCanvas;
	public GameObject gameWinCanvas;
	public Button restartButton;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public bool isGameOver = false;
	void Start()
	{
		isGameOver = false;
		gameWinCanvas.SetActive(false);
		gameOverCanvas.SetActive(false);
		restartButton.onClick.AddListener(RestartGame);
	}

	public void GameOver()
	{
		isGameOver = true;
		gameOverCanvas.SetActive(true);
		StopTime();
	}

	public void GameWin()
	{
		gameWinCanvas.SetActive(true);
		Invoke("LoadNextLevel", 4f);
	}

	public void RestartGame()
	{
		isGameOver = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadNextLevel()
	{
		int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextBuildIndex < SceneManager.sceneCountInBuildSettings)
		{
			isGameOver = false;
			Time.timeScale = 1f;
			SceneManager.LoadScene(nextBuildIndex);
		}
		else
		{
			isGameOver = true;
			StopTime();
			Debug.LogWarning("No more levels available.");
		}
	}

	public void StopTime()
	{
		Time.timeScale = 0f;
	}
}
