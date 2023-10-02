using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivePerk", menuName = "Assets/Perks/newActivePerk")]
public class ActivePerk : Perk
{
	[SerializeField] private List<PerkEffect> _perkEffects;

	public override void ActivateEffects(Enemy _enemy)
	{
		foreach (var effect in _perkEffects)
		{
			effect.damageBonuses = _damageBonuses;
			effect.ActivateEffect(_enemy);
		}
	}

	public override void ReActivateEffects(Enemy _enemy)
	{
		DamageHandler _handler = _enemy.GetComponent<DamageHandler>();
		foreach (var effect in _perkEffects)
		{
			if (_handler.CurrentEffects.Contains(effect) || effect.Instance)
			{
				continue;
			}
			effect.damageBonuses = _damageBonuses;
			effect.ActivateEffect(_enemy);
		}
	}
	
}