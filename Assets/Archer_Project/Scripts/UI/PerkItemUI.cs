using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkItemUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _perkName;
	[SerializeField] private TextMeshProUGUI _perkLVL;
	[SerializeField] private Image _perkImage;
	[SerializeField] private TextMeshProUGUI _lvlText;
	private LevelUpUI _levelUp;

	private void Awake()
	{
		_levelUp = FindObjectOfType<LevelUpUI>();
	}

	public void SelectItem()
	{
		Debug.Log("selected ID" + PerkItemID);
		_levelUp.SelectedItemId = PerkItemID;
		_levelUp.SetDescription(Discription);
	}

	public int PerkItemID { get; set; }
	public String Discription { get; set; }

	public void SetPerkData(string name, string discription, Sprite icon, int perkLevel, int _id = 0)
	{
		_perkName.text = name;
		_perkImage.sprite = icon;
		Discription = discription;
		PerkItemID = _id;
		_perkLVL.text = perkLevel.ToString();
	}

	public void HideLvL()
	{
		_lvlText.enabled = false;
		_perkLVL.enabled = false;
	}
}