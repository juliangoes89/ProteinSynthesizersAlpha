using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ScoreRecord
{
	public int Id;
	public int deaths;
	public int levels;
	public int isFinished;
	public string endDate;
	public string session_id;
}

[System.Serializable]
public class ScoreResponse
{
	public ScoreRecord record;
}

public class ScorePostRequest
{
	public string session_id;
}

public class ScorePutGameOverRequest
{
	public string session_id;
	public int deaths;
}

public class ScorePutLevelRequest
{
	public string session_id;
	public int levels;
}

public class ScorePutGameFinishedRequest
{
	public string session_id;
	public int levels;
	public int isFinished;
	public string endDate;
}

public class GameManager : MonoBehaviour
{
	public GameObject gameOverCanvas;
	public GameObject gameWinCanvas;
	public Button restartButton;
	private string? sessionId;
	private int gameOverCount = 0;
	private int levelCount = 0;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public bool isGameOver = false;
	async void Start()
	{
		isGameOver = false;
		gameOverCanvas.SetActive(false);
		gameWinCanvas.SetActive(false);
		restartButton.onClick.AddListener(RestartGame);
		sessionId = PlayerPrefs.GetString("sessionId");
		Debug.Log("Session ID: " + sessionId);
		if (string.IsNullOrEmpty(sessionId))
		{
			sessionId = Guid.NewGuid().ToString(); // Genera un nuevo ID de sesión
			PlayerPrefs.SetString("sessionId", sessionId);
			PlayerPrefs.SetInt("gameOverCount", gameOverCount);
			PlayerPrefs.SetInt("levelCount", levelCount);
			Debug.Log("Session ID: " + sessionId);
			ScorePostRequest requestData = new ScorePostRequest
			{
				session_id = sessionId
			};
			Debug.Log("Session ID: " + requestData.session_id);
			string jsonData = JsonUtility.ToJson(requestData, true);
			Debug.Log("JSON Data: " + jsonData);
			byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
			UnityWebRequest www = new UnityWebRequest("https://juliangoes89.website:8443/post_scores.php", "POST");
			www.uploadHandler = new UploadHandlerRaw(postData);
			www.downloadHandler = new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");
			await www.SendWebRequest();
			Debug.Log("Request sent: " + www.downloadHandler.text);
		}
		else
		{
			if(PlayerPrefs.HasKey("gameOverCount"))
			{
				gameOverCount = PlayerPrefs.GetInt("gameOverCount");
			}
			else
			{
				gameOverCount = 0;
			}
			if(PlayerPrefs.HasKey("levelCount"))
			{
				levelCount = PlayerPrefs.GetInt("levelCount");
			}
			else
			{
				levelCount = 0;
			}
		}
	}

	public void GameOver()
	{
		isGameOver = true;
		gameOverCanvas.SetActive(true);
		StopTime();
		gameOverCount++;
		PlayerPrefs.SetInt("gameOverCount", gameOverCount);
		sessionId = PlayerPrefs.GetString("sessionId");
		if (sessionId != null)
		{

			ScorePutGameOverRequest requestData = new ScorePutGameOverRequest
			{
				session_id = sessionId,
				deaths = gameOverCount
			};
			string jsonData = JsonUtility.ToJson(requestData);
			byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
			UnityWebRequest www = new UnityWebRequest("https://juliangoes89.website:8443/put_scores.php", "POST");
			www.uploadHandler = new UploadHandlerRaw(postData);
			www.downloadHandler = new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");
			www.SendWebRequest();

			if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError("Error: " + www.error);
			}
			else
			{
				Debug.Log("Score submitted successfully.");
			}
		}
		else
		{
			Debug.LogWarning("Session ID is null. Cannot submit score.");
		}
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
		levelCount++;
		PlayerPrefs.SetInt("levelCount", levelCount);
		int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextBuildIndex < SceneManager.sceneCountInBuildSettings)
		{
			sessionId = PlayerPrefs.GetString("sessionId");
			//Incrementa el contador de niveles en PlayerPrefs y lo envia al servidor
			if (sessionId != null)
			{
				ScorePutLevelRequest requestData = new ScorePutLevelRequest
				{
					session_id = sessionId,
					levels = levelCount
				};
				string jsonData = JsonUtility.ToJson(requestData);
				byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
				UnityWebRequest www = new UnityWebRequest("https://juliangoes89.website:8443/put_scores.php", "POST");
				www.uploadHandler = new UploadHandlerRaw(postData);
				www.downloadHandler = new DownloadHandlerBuffer();
				www.SetRequestHeader("Content-Type", "application/json");
				www.SendWebRequest();

				if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
				{
					Debug.LogError("Error: " + www.error);
				}
				else
				{
					Debug.Log("Score Level submitted successfully.");
				}
			}
			else
			{
				Debug.LogWarning("Session ID is null. Cannot submit score Level.");
			}
			isGameOver = false;
			Time.timeScale = 1f;
			SceneManager.LoadScene(nextBuildIndex);
		}
		else
		{
			sessionId = PlayerPrefs.GetString("sessionId");
			if (sessionId != null)
			{
				ScorePutGameFinishedRequest requestData = new ScorePutGameFinishedRequest
				{
					session_id = sessionId,
					levels = levelCount,
					isFinished = 1,
					endDate = DateTime.Now.AddHours(5).ToString("yyyy-MM-dd HH:mm:ss")
				};
				string jsonData = JsonUtility.ToJson(requestData);
				byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
				UnityWebRequest www = new UnityWebRequest("https://juliangoes89.website:8443/put_scores.php", "POST");
				www.uploadHandler = new UploadHandlerRaw(postData);
				www.downloadHandler = new DownloadHandlerBuffer();
				www.SetRequestHeader("Content-Type", "application/json");
				www.SendWebRequest();

				if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
				{
					Debug.LogError("Error: " + www.error);
				}
				else
				{
					Debug.Log("Score Level submitted successfully.");
				}
			}
			else
			{
				Debug.LogWarning("Session ID is null. Cannot submit score Level.");
			}

			// No hay más niveles disponibles
			isGameOver = true;
			StopTime();
			Debug.LogWarning("No more levels available.");
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
		}
	}

	public void StopTime()
	{
		Time.timeScale = 0f;
	}
	// Add the OnApplicationQuit method to the GameManager class
	private void OnApplicationQuit()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
}
