using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivePerk : Perk
{
	[Header("Bonus Characteristic")] [SerializeField]
	private float _hpBonus;

	[SerializeField] private float _arrowSpeedBonus;
	[SerializeField] private float _attackSpeedBonus;


	[Header("Bonus Damage")] [SerializeField]
	private float _physicsDamage;

	[SerializeField] private float _fireDamage;
	[SerializeField] private float _iceDamage;
	[SerializeField] private float _poisonDamage;
	[SerializeField] private float _electricDamage;

	[Header("Bonus Protection")] [SerializeField]
	private float _magickShiled;

	[SerializeField] private float _fireProtection;
	[SerializeField] private float _iceProtection;
	[SerializeField] private float _poisonProtection;
	
	
	public override bool Activate(Archer _archer)
	{
		_archer.PhysicsDamage += _physicsDamage;
		_archer.FireDamage += _fireDamage;
		_archer.IceDamage += _iceDamage;
		_archer.PoisonDamage += _poisonDamage;
		_archer.ElectricDamage += _electricDamage;
		return true;
	}
	
	public override void DeActivate(Archer _archer)
	{
		_archer.PhysicsDamage -= _physicsDamage;
		_archer.FireDamage -= _fireDamage;
		_archer.IceDamage -= _iceDamage;
		_archer.PoisonDamage -= _poisonDamage;
		_archer.ElectricDamage -= _electricDamage;
	}

	public override void ActivateEffects(Enemy _enemy)
	{
		throw new System.NotImplementedException();
	}
}
