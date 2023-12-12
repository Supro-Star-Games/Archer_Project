using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DownMenuUI : MonoBehaviour
{
	[SerializeField] private Button _attackMenuButton;
	[SerializeField] private Button _defenceMenuButton;
	[SerializeField] private Button _economyButton;
	[SerializeField] private Transform _openPosition;
	[SerializeField] private Transform _content;
	[SerializeField] private TalentItem _talentTemplate;
	[SerializeField] private TextMeshProUGUI _currentEXP;
	[SerializeField] private TextMeshProUGUI _currentCoins;

	private Archer _archer;
	private PlayerStats _playerStats;

	private Vector3 _hiddenPosition;
	private bool isOpened;
	private int currentMenuID;
	private List<PlayerStats.Stat> _currentStats = new List<PlayerStats.Stat>();
	private List<TalentItem> _currentItems = new List<TalentItem>();
	private PlayerStats.MenuType _menuType;


	private void OnEnable()
	{
		_attackMenuButton.onClick.AddListener(OpenHiddenContent);
		_hiddenPosition = transform.position;
		PlayerCurrencies.Instance.OnEXPAdded += ChangeExpView;
		_archer = FindObjectOfType<Archer>();
		_playerStats = FindObjectOfType<PlayerStats>();
	}

	private void OnDisable()
	{
		_attackMenuButton.onClick.RemoveListener(OpenHiddenContent);
		PlayerCurrencies.Instance.OnEXPAdded -= ChangeExpView;
	}

	private void Start()
	{
		LoadCurrentPerks();
	}

	private void LoadCurrentPerks()
	{
		_currentStats = _playerStats.Stats;
		/*
		List<Perk> perks = _archer.GetLernedPerks();
		List<PassivePerk> passivePerks = new List<PassivePerk>();
		foreach (var perk in perks)
		{
			if (perk is PassivePerk)
			{
				passivePerks.Add(perk as PassivePerk);
			}
		}
		*/

		foreach (var stat in _currentStats)
		{
			TalentItem _newItem = Instantiate(_talentTemplate, _content);
			_newItem.SetItem(stat);
			_currentItems.Add(_newItem);
		}
	}

	public void CurrentMenuOpen(int menuID)
	{
		currentMenuID = menuID;
		switch (currentMenuID)
		{
			case 0:
				_menuType = PlayerStats.MenuType.Attack;
				break;
			case 1:
				_menuType = PlayerStats.MenuType.Defence;
				break;
			case 2:
				_menuType = PlayerStats.MenuType.Economy;
				break;
		}
	}

	private void ChangeExpView(int value)
	{
		_currentEXP.text = value.ToString();
	}

	private void OpenHiddenContent()
	{
		if (isOpened)
		{
			transform.DOMove(_hiddenPosition, 0.5f);
			isOpened = false;
		}
		else
		{
			transform.DOMove(_openPosition.position, 0.5f);
			isOpened = true;
		}
	}
}