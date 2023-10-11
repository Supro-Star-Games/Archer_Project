using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PassivePerk", menuName = "Assets/Perks/newPassivePerk")]
public class PassivePerk : Perk
{
	[FormerlySerializedAs("bounsInPercents")] [Header("Passive Bonus")] [SerializeField]
	private float bonusInPercents;

	[SerializeField] private BonusStatType _bonusStatType;


	public enum BonusStatType
	{
		HitPoints,
		ArrowSpeed,
		AttackSpeed,
		PhysicsDamage,
		FireDamage,
		IceDamage,
		PoisonDamage,
		ElectricDamage,
		PhysicsProtection,
		FireProtection,
		IceProtection,
		ElectricProtection,
		PoisonProtection
	}


	public override void PassiveActivate(Archer _archer)
	{
		Debug.Log("passive Activate");
		_archer.TakePassivePerk(_bonusStatType, bonusInPercents);
	}

	public override void StartActivate(Archer _archer)
	{
		_archer.TakePassivePerk(_bonusStatType, bonusInPercents * perkLVL);
	}

	public override void ActivateEffects(Enemy _enemy)
	{
		
	}

	public override void ImprovePerk()
	{
		perkLVL += 1;
	}
}