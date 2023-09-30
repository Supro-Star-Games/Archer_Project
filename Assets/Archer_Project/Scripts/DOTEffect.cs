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
		Fire = 1,
		Ice = 2,
		Poison = 3,
		Electric = 4,
		Physics = 5
	}

	[SerializeField] public DamageType damageType;

	public override void ActivateEffect(Enemy _enemy = null)
	{
		if (_enemy == null)
		{
			return;
		}
		float damagePerTick = _damage / _ticks;
		float tickDelay = _duration / _ticks;
		_damageHandler = _enemy.GetComponent<DamageHandler>();
		_damageHandler.Activate(_enemy, damagePerTick, tickDelay, _ticks, damageType);
	}

	
}