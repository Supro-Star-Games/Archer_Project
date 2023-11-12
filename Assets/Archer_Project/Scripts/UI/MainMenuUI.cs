using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _settingsButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private LevelSelectUI _levelSelect;

	private void OnEnable()
	{
		_startButton.onClick.AddListener(GameManager.StartNewGame);
		_exitButton.onClick.AddListener(GameManager.ExitGame);
		_continueButton.onClick.AddListener(SelectLevelOpen);
	}

	private void OnDisable()
	{
		_startButton.onClick.RemoveListener(GameManager.StartNewGame);
		_exitButton.onClick.RemoveListener(GameManager.ExitGame);
		_continueButton.onClick.RemoveListener(SelectLevelOpen);
	}

	private void SelectLevelOpen()
	{
		_levelSelect.Open();
	}
}