using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffEfect", menuName = "Assets/PerkEffects/newDebuffEffect")]
public class DebuffEffect : PerkEffect
{
	[SerializeField] private float _duration;
	[SerializeField] private float _percent;
	[SerializeField] private Parameter _characterisric;

	private DamageHandler _damageHandler;

	public enum Parameter
	{
		AttackDamage,
		AttackSpeed,
		MoveSpeed,
		PhysicsProtection,
		FireProtection,
		IceProtection,
		PoisonProtection,
		ElectricProtection
	}


	public override void ActivateEffect(Enemy _enemy = null)
	{
		if (_enemy != null)
		{
			_damageHandler = _enemy.GetComponent<DamageHandler>();
			_damageHandler.HandleDebuff(_enemy, _duration, _percent, _characterisric);
		}
	}
}