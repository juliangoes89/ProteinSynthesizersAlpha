using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoPlayerCollisionHandler : MonoBehaviour
{
	[SerializeField] float levelLoadDelay = 2f;
	[SerializeField] AudioClip explosionSound;
	[SerializeField] AudioClip finishSound;

	[SerializeField] ParticleSystem explosionParticles;
	[SerializeField] ParticleSystem finishParticles;

	AudioSource audioSource;

	public bool isTransitioning = false;

	Dictionary<string, string> antiCodonsAndCodonsDictionary = new Dictionary<string, string>();

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		antiCodonsAndCodonsDictionary.Add("UAC", "AUG");
		antiCodonsAndCodonsDictionary.Add("UGG", "ACC");
		antiCodonsAndCodonsDictionary.Add("UUG", "AAC");
		antiCodonsAndCodonsDictionary.Add("ACU", "UGA");
	}

	void OnCollisionEnter(Collision other)
	{
		// Ignore collisions when transitioning
		if (isTransitioning)
		{
			return;
		}

		// Ignore collisions with friendly objects
		if (other.gameObject.tag == "Friendly"|| other.gameObject.tag.Contains("AminoAcid")) {
			return;
		}

		string currentCodonTag = "";
		GameObject rocketControllerObject = GameObject.Find("RocketController");
		TwoPlayerRocketController rocketController = rocketControllerObject.GetComponent<TwoPlayerRocketController>();
		currentCodonTag = rocketController.currentCodonTag;

		if (antiCodonsAndCodonsDictionary[currentCodonTag] == other.gameObject.tag)
		{
			StartNextLevelSequence(other);
			return;
		}
		else { 
			StartCrashSequence(other);
		}
		
		
	}

	private void StartCrashSequence(Collision other)
	{
		isTransitioning = true;

		audioSource.Stop();
		PlayerOneMovement scriptToDetach = GetComponent<PlayerOneMovement>();
		scriptToDetach.stopAllParticles();
		Destroy(scriptToDetach);
		audioSource.PlayOneShot(explosionSound);
		explosionParticles.Play();
		gameObject.AddComponent<PlayerTwoMover>();
		//other.transform.parent = gameObject.transform;
		//Invoke("ReloadLevel", levelLoadDelay);
	}

	private void ReloadLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
	}

	private void StartNextLevelSequence(Collision other)
	{
		isTransitioning = true;

		audioSource.Stop();
		PlayerOneMovement scriptToDetach = GetComponent<PlayerOneMovement>();
		scriptToDetach.stopAllParticles();
		Destroy(scriptToDetach); 
		audioSource.PlayOneShot(finishSound);
		finishParticles.Play();
		gameObject.transform.parent = other.transform.parent;
		//other.transform.parent = gameObject.transform;
		//Invoke("NextLevel", levelLoadDelay);
	}

	private void NextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
		{
			currentSceneIndex = 0;
		}
		else
		{
			currentSceneIndex++;
		}
		SceneManager.LoadScene(currentSceneIndex);
	}
}
