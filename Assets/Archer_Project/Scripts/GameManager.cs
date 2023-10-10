using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public static bool IsPaused;
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		
	}
	
	public static void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public static void StartNewGame()
	{
		SceneManager.LoadScene(1);
	}

	public static void ExitGame()
	{
		Application.Quit();
	}

	public static void LoadMenu()
	{
		SceneManager.LoadScene(0);
	}

	public static void PauseGame()
	{
		if (Time.timeScale > 0)
		{
			IsPaused = true;
			Time.timeScale = 0f;
		}
		else
		{
			IsPaused = false;
			Time.timeScale = 1f;
		}
	}
}