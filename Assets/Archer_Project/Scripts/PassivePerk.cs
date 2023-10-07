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
		if (isActivated == false)
		{
			Debug.Log("passive Activate");
			_archer.TakePassivePerk(_bonusStatType, bonusInPercents);
			isActivated = true;
		}
	}

	public override void DeActivate(Archer _archer)
	{
		isActivated = false;
	}

	public override void ActivateEffects(Enemy _enemy)
	{
		throw new System.NotImplementedException();
	}

	public override void ImprovePerk()
	{
		bonusInPercents += bonusInPercents;
		perkLVL += 1;
	}
}