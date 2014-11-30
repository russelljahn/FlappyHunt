using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static long numKilled;
	public static float timeRemaining;
	public static float maxTimeRemaining = 60;
	

	public static GameObject titleOverlay;
	public static GameObject gameOverOverlay;

	public static bool gameOver = false;
	public static bool mainMenu = true;
	

	// Use this for initialization
	void Start () {
		timeRemaining = maxTimeRemaining;

		titleOverlay = GameObject.FindWithTag("Title");
		gameOverOverlay = GameObject.FindWithTag("GameOver");
		gameOverOverlay.SetActive(false);
		
		Time.timeScale = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;

		if (timeRemaining <= 0) {
//			Time.timeScale = 0.0f;
			gameOverOverlay.gameObject.SetActive(true);
			gameOver = true;
		}
	}



	public static void HandleTitleScreenTouch() {
		GameManager.titleOverlay.SetActive(false);
		Time.timeScale = 1.0f;
		GameManager.mainMenu = false;
		GameManager.gameOver = false;
	}



	public static void HandleGameOverScreenTouch() {
		GameManager.numKilled = 0;
		GameManager.timeRemaining = maxTimeRemaining;
		GameManager.gameOver = false;
		GameManager.mainMenu = true;
		
		GameManager.gameOverOverlay.SetActive(false);
		GameManager.titleOverlay.SetActive(true);
		
		
		Application.LoadLevel(Application.loadedLevel);
	}



}
