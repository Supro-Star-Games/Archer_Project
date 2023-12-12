using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class LevelUpUI : MonoBehaviour
{
	[SerializeField] private Transform _learnedPerksContent;
	[SerializeField] private Transform[] _perksToSelect;
	[SerializeField] private List<Perk> _perks;
	[SerializeField] private Button _selectButton;
	[SerializeField] private Archer _archer;
	[SerializeField] private PerkItemUI _perkItem;
	[SerializeField] private TextMeshProUGUI _description;

	private List<Perk> _randomizedPerks = new List<Perk>();
	private List<PerkItemUI> _perkItems = new List<PerkItemUI>();
	private List<Perk> _learnedPerks = new List<Perk>();

	private bool isSelected;

	private List<int> _randomizedPerkIndexes = new List<int>();
	public int SelectedItemId { get; set; }

	private void OnEnable()
	{
		RandomizePerks();
		CreatePerkItems();
		_description.text = _randomizedPerks[0].PerkDescription;
		SetupLearnedPerks();
	}

	private void Awake()
	{ 
	//	_archer.ArhcerLVLUp += Show;
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
	//	_archer.ArhcerLVLUp -= Show;
	}

	public void SetupLearnedPerks()
	{
		_learnedPerks = _archer.GetLernedPerks();
		foreach (var perk in _learnedPerks)
		{
			PerkItemUI _newItem = Instantiate(_perkItem, _learnedPerksContent);
			_newItem.SetPerkData(perk.PerkName, perk.PerkDescription, perk.PerkIcon, perk.PerkLVL);
			_perkItems.Add(_newItem);
		}
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	public void RandomizePerks()
	{
		_randomizedPerks.Clear();
		for (int i = 0; i < _perksToSelect.Length; i++)
		{
			//	int value = Random.Range(0, _perks.Count - 1);
			int value = GameManager.GenerateRandomNumber(0, _perks.Count - 1, _randomizedPerkIndexes);
			_randomizedPerks.Add(_perks[value]);
			_randomizedPerkIndexes.Add(value);
			//	_perks.RemoveAt(value);
		}
	}

	public void CreatePerkItems()
	{
		for (int i = 0; i < _randomizedPerks.Count; i++)
		{
			PerkItemUI _newitem = Instantiate(_perkItem, _perksToSelect[i]);
			_newitem.SetPerkData(_randomizedPerks[i].PerkName, _randomizedPerks[i].PerkDescription, _randomizedPerks[i].PerkIcon, _randomizedPerks[i].PerkLVL, i);
			_newitem.HideLvL();
			_perkItems.Add(_newitem);
		}
	}

	public void SetDescription(String _text)
	{
		_description.text = _text;
	}

	public void SelectItem()
	{
		if (!isSelected)
		{
			_archer.LearnPerk(_randomizedPerks[SelectedItemId]);
			_randomizedPerks.RemoveAt(SelectedItemId);
			Destroy(_perkItems[SelectedItemId].gameObject);
			_perks.RemoveAt(_randomizedPerkIndexes[SelectedItemId]);
			ClearPerkItems();
			SetupLearnedPerks();
			isSelected = true;
		}
		//	_perkItems[SelectedItemId].transform.SetParent(_learnedPerksContent);
	}

	public void ClearPerkItems()
	{
		foreach (var item in _perkItems)
		{
			Destroy(item.gameObject);
		}

		_perkItems.Clear();
	}

	private void OnDisable()
	{
		ClearPerkItems();

		foreach (var perk in _randomizedPerks)
		{
			_perks.Add(perk);
		}

		_randomizedPerks.Clear();
		_randomizedPerkIndexes.Clear();

		isSelected = false;
	}

}