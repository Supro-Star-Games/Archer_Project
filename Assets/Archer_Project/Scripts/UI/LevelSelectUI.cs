using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
	[SerializeField] private Button _level1;
	[SerializeField] private Button _level2;
	[SerializeField] private Button _level3;
	[SerializeField] private Button _back;
	private CanvasGroup _canvasGroup;

	private void OnEnable()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_level1.onClick.AddListener(LoadLevel1);
		_level2.onClick.AddListener(LoadLevel2);
		_level3.onClick.AddListener(LoadLevel3);
		_back.onClick.AddListener(Close);
	}

	private void OnDisable()
	{
		_level1.onClick.RemoveListener(LoadLevel1);
		_level2.onClick.RemoveListener(LoadLevel2);
		_level3.onClick.RemoveListener(LoadLevel3);
		_back.onClick.RemoveListener(Close);
	}

	public void Open()
	{
		_canvasGroup.alpha = 1;
		_canvasGroup.interactable = true;
		_canvasGroup.blocksRaycasts = true;
	}

	public void Close()
	{
		_canvasGroup.alpha = 0;
		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;
	}

	public void LoadLevel1()
	{
		SceneManager.LoadScene(1);
	}

	public void LoadLevel2()
	{
		SceneManager.LoadScene(2);
	}

	public void LoadLevel3()
	{
		SceneManager.LoadScene(3);
	}
}