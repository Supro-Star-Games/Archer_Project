using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private Button _menuButton;
	[SerializeField] private Button _restartButton;

	private Archer _archer;
	private Fence _fence;

	private void Awake()
	{
		_archer = FindObjectOfType<Archer>();
		_fence = FindObjectOfType<Fence>();
		_menuButton.onClick.AddListener(GameManager.LoadMenu);
		_restartButton.onClick.AddListener(GameManager.RestartLevel);
		_menuButton.onClick.AddListener(GameManager.PauseGame);
		_restartButton.onClick.AddListener(GameManager.PauseGame);
		_archer.GameOver += ShowUI;
		_fence.FenceDestroed += ShowUI;
		gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		GameManager.PauseGame();
	}


	private void OnDestroy()
	{
		_menuButton.onClick.RemoveListener(GameManager.LoadMenu);
		_restartButton.onClick.RemoveListener(GameManager.RestartLevel);
		_menuButton.onClick.RemoveListener(GameManager.PauseGame);
		_restartButton.onClick.RemoveListener(GameManager.PauseGame);
		_archer.GameOver -= ShowUI;
		_fence.FenceDestroed -= ShowUI;
	}

	private void ShowUI()
	{
		gameObject.SetActive(true);
	}
}