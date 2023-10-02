using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour

{
	public List<PerkEffect> CurrentEffects = new List<PerkEffect>();
	private Enemy effectTarget;

	private void Awake()
	{
		effectTarget = GetComponent<Enemy>();
	}

	public void Activate(float damagePerTick, float tickDelay, int _ticks, DOTEffect.DamageType _type, DOTEffect effect)
	{
		CurrentEffects.Add(effect);
		StartCoroutine(TickDamage(effectTarget, damagePerTick, tickDelay, _ticks, _type, effect));
	}

	public void HandleDebuff(float duration, float percent, DebuffEffect.Parameter parameter, DebuffEffect debuff)
	{
		CurrentEffects.Add(debuff);
		StartCoroutine(Debuff(effectTarget, duration, percent, parameter, debuff));
	}

	IEnumerator TickDamage(Enemy effectTarget, float damagePerTick, float tickDelay, int _ticks, DOTEffect.DamageType _type, DOTEffect _effect)
	{
		for (int ticks = 0; ticks < _ticks; ticks++)
		{
			effectTarget.TakeDamage(damagePerTick, _type);
			yield return new WaitForSeconds(tickDelay);
			if (ticks == _ticks - 1)
			{
				CurrentEffects.Remove(_effect);
			}
		}
	}

	IEnumerator Debuff(Enemy effectTarget, float duration, float percent, DebuffEffect.Parameter parameter, DebuffEffect _debuff)
	{
		effectTarget.TakeDebuff(parameter, percent);
		yield return new WaitForSeconds(duration);
		effectTarget.TakeDebuff(parameter, -percent);
		CurrentEffects.Remove(_debuff);
		yield break;
	}
}