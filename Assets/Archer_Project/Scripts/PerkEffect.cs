using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PerkEffect : ScriptableObject
{
	[SerializeField] protected String _effectName;
	[SerializeField] private bool _instance;

	public List<float> damageBonuses = new List<float>();
	public bool Instance => _instance;
	public abstract void ActivateEffect(Enemy _enemy = null);

	public virtual DOTEffect.DamageType GetDamageType()
	{
		return 0;
	}

}
