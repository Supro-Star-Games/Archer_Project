using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TalentItem : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _talentName;
	[SerializeField] private TextMeshProUGUI _talentCost;
	[SerializeField] private TextMeshProUGUI _curentValue;
	[SerializeField] private TextMeshProUGUI _addValue;
	[SerializeField] private Button _buyButton;

	private PlayerStats.Stat _currentStat;

	private ArcherUI _archerUI;


	private void Awake()
	{
		_archerUI = FindObjectOfType<ArcherUI>();
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		_buyButton.onClick.AddListener(TryBuyTalent);
	}

	private void OnDisable()
	{
		_buyButton.onClick.RemoveListener(TryBuyTalent);
	}

	public void TryBuyTalent()
	{
		if (PlayerCurrencies.Instance.TryBuyForExp(_currentStat.Cost))
		{
			_currentStat.IncreaseStat();
			SetItem(_currentStat);
		}
		else
		{
			Debug.Log("Не хватает опыта");
		}
	}

	private void OnDestroy()
	{
		_currentStat.OnStatChanged -= _archerUI.ChangeStatsView;
	}

	public void SetItem(PlayerStats.Stat _stat)
	{
		_talentName.text = _stat.Name;
		_talentCost.text = _stat.Cost.ToString();
		_curentValue.text = _stat.Value.ToString();
		_addValue.text = _stat.ValueIncrease.ToString();
		_stat.OnStatChanged += _archerUI.ChangeStatsView;
		_currentStat = _stat;
	}
}