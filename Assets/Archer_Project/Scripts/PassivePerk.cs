using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PassivePerk", menuName = "Assets/Perks/newPassivePerk")]
public class PassivePerk : Perk
{
	[FormerlySerializedAs("bounsInPercents")] [Header("Passive Bonus")] [SerializeField]
	private float bonusInPercents;

//	[SerializeField] private BonusStatType _bonusStatType;
//	[SerializeField] public BaseStatType _baseStatType;
	[SerializeField] private int _priceGrowPercent;

	public int Price;

//	public BonusStatType StatType => _bonusStatType;

	private void Awake()
	{
		CurrentPrice = _basePerkCost;
		Price = _basePerkCost;
	}
	

	public override void PassiveActivate(Archer _archer)
	{
		Debug.Log("passive Activate");
//		_archer.TakePassivePerk(_bonusStatType, bonusInPercents);
		float newPrice = CurrentPrice + (float)CurrentPrice / 100 * _priceGrowPercent;
		CurrentPrice = Mathf.CeilToInt(newPrice);
	}

	public override void StartActivate(Archer _archer)
	{
//		_archer.TakePassivePerk(_bonusStatType, bonusInPercents * perkLVL);
	}

	public override void ActivateEffects(Enemy _enemy)
	{
	}

	public override void ImprovePerk()
	{
		perkLVL += 1;
	}
}