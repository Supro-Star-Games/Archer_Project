using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkItemUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _perkName;
	[SerializeField] private Image _perkImage;
	
	public String Discription { get; set; }

	public void SetPerkData(string name, string discription, Sprite icon)
	{
		_perkName.text = name;
		_perkImage.sprite = icon;
		Discription = discription;
	}
}