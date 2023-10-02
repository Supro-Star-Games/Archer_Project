using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "InstanceDamageEffect", menuName = "Assets/PerkEffects/newInstanceDamageEffect")]
public class InstanceDamageEffect : PerkEffect
{
	[SerializeField] private float _minDamage;
	[SerializeField] private float _maxDamage;
	[SerializeField] private DOTEffect.DamageType _damageType;
	
	public override void ActivateEffect(Enemy _enemy = null)
	{
		
		float damage = Random.Range(_minDamage, _maxDamage);
		float totalDamage = damage + damage * (damageBonuses[(int)_damageType] / 100f);
		_enemy.TakeDamage(totalDamage, _damageType);
		
	}
}