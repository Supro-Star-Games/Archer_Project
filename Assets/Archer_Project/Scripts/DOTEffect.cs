using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DOTEffect", menuName = "Assets/PerkEffects/newDOTEffect")]
public class DOTEffect : PerkEffect
{
	[SerializeField] private float _damage;
	[SerializeField] private float _duration;
	[SerializeField] private int _ticks;

	public DamageHandler _damageHandler;

	public enum DamageType
	{
		Fire = 0,
		Ice = 1,
		Poison = 2,
		Electric = 3,
		Physics = 4
	}

	[SerializeField] public DamageType damageType;

	public override void ActivateEffect(Enemy _enemy = null)
	{
		if (_enemy == null)
		{
			return;
		}

		float totalDamage =_damage + _damage * (damageBonuses[(int)damageType] / 100f);
		float damagePerTick = totalDamage / _ticks;
		float tickDelay = _duration / _ticks;
		_damageHandler = _enemy.GetComponent<DamageHandler>();
		_damageHandler.Activate(damagePerTick, tickDelay, _ticks, damageType, this);
	}
}