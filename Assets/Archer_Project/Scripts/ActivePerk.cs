using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivePerk", menuName = "Assets/Perks/newActivePerk")]
public class ActivePerk : Perk
{
	[SerializeField] private List<PerkEffect> _perkEffects;

	public override void ActivateEffects(Enemy _enemy)
	{
		Debug.Log("activateEffect");
		foreach (var effect in _perkEffects)
		{
			effect.ActivateEffect(_enemy);
		}
	}
}