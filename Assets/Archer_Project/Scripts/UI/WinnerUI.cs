using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerUI : MonoBehaviour
{
	[SerializeField] private Button _nextStageButton;
	private SpawnRandomizer _spawnRandomizer;


	private void Awake()
	{
		_spawnRandomizer = FindObjectOfType<SpawnRandomizer>();
		_spawnRandomizer.EnemiesDead += Show;
		_nextStageButton.onClick.AddListener(GameManager.RestartLevel);
		gameObject.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void OnDestroy()
	{
		_spawnRandomizer.EnemiesDead -= Show;
	}
}