using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour

{
	public void Activate(Enemy effectTarget, float damagePerTick, float tickDelay, int _ticks, DOTEffect.DamageType _type)
	{
		Debug.Log("ActivateCoroutine");
		StartCoroutine(TickDamage(effectTarget, damagePerTick, tickDelay, _ticks, _type));
	}

	public void HandleDebuff(Enemy effectTarget, float duration, float percent, DebuffEffect.Parameter parameter)
	{
		StartCoroutine(Debuff(effectTarget, duration, percent, parameter));
	}

	IEnumerator TickDamage(Enemy effectTarget, float damagePerTick, float tickDelay, int _ticks, DOTEffect.DamageType _type)
	{
		for (int ticks = 0; ticks < _ticks; ticks++)
		{
			effectTarget.TakeDamage(damagePerTick, _type);
			yield return new WaitForSeconds(tickDelay);
		}
	}

	IEnumerator Debuff(Enemy effectTarget, float duration, float percent, DebuffEffect.Parameter parameter)
	{
		effectTarget.TakeDebuff(parameter, percent);
		yield return new WaitForSeconds(duration);
		effectTarget.TakeDebuff(parameter, -percent);
		yield break;
	}
}