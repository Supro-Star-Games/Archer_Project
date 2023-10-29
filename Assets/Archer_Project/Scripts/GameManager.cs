using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	[SerializeField] private int _appFPS;
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

		Application.targetFrameRate = _appFPS;
	}

	public static Perk CopyScriptable(Perk copyedPerk)
	{
		Perk copiedObject = ScriptableObject.CreateInstance<Perk>();
		copiedObject = copyedPerk;

		//	AssetDatabase.CreateAsset(copiedObject, "Assets/CopiedScriptableObject.asset");
		//	 AssetDatabase.SaveAssets();

		return copiedObject;
	}

	public static int GenerateRandomNumber(int minValue, int maxValue, List<int> uniqueNumbers)
	{
		if (uniqueNumbers.Count >= (maxValue - minValue + 1))
		{
			// Все уникальные числа в диапазоне исчерпаны.
			return -1; // Или можно выбрать другой способ обработки.
		}

		int randomNum;
		do
		{
			randomNum = Random.Range(minValue, maxValue + 1);
		} while (uniqueNumbers.Contains(randomNum));

		return randomNum;
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