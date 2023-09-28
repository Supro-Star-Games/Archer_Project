using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk", menuName = "Assets/Perks/newPerk")]
public class Perk : ScriptableObject
{
	[Header("Perk Data")] [SerializeField] private Sprite _perkIcon;
	[SerializeField] private String _perkName;
	[SerializeField] private String _perkDescription;
	[SerializeField] private float chanceToProke;

	[Header("Bonus Characteristic")] [SerializeField]
	private float _hpBonus;

	[SerializeField] private float _arrowSpeedBonus;
	[SerializeField] private float _attackSpeedBonus;


	[Header("Bonus Damage")] [SerializeField]
	private float physicsDamage;

	[SerializeField] private float _fireDamage;
	[SerializeField] private float _iceDamage;
	[SerializeField] private float _poisonDamage;
	[SerializeField] private float _magickDamage;

	[Header("Bonus Protection")] [SerializeField]
	private float _magickShiled;

	[SerializeField] private float _fireProtection;
	[SerializeField] private float _iceProtection;
	[SerializeField] private float _poisonProtection;

	public void Initilize(Archer _archer)
	{
	}
}