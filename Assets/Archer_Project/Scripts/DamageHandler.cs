using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour

{
	private DamageHandler _damageHandler;

	public void Activate(Enemy effectTarget, float damagePerTick, float tickDelay, int _ticks, DOTEffect.DamageType _type)
	{
		Debug.Log("ActivateCoroutine");
		StartCoroutine(TickDamge(effectTarget, damagePerTick, tickDelay, _ticks, _type));
	}

	public void HandleDebuff(Enemy effectTarget, float duration, float percent, DebuffEffect.Parameter parameter)
	{
		StartCoroutine(Debuff(effectTarget, duration, percent, parameter));
	}

	IEnumerator TickDamge(Enemy effectTarget, float damagePerTick, float tickDelay, int _ticks, DOTEffect.DamageType _type)
	{
		for (int ticks = 0; ticks < _ticks; ticks++)
		{
			switch (_type)
			{
				case DOTEffect.DamageType.Fire:
				{
					effectTarget.TakeDamage(0, damagePerTick, 0, 0, 0);
					break;
				}
				case DOTEffect.DamageType.Ice:
				{
					effectTarget.TakeDamage(0, 0, damagePerTick, 0, 0);
					break;
				}
				case DOTEffect.DamageType.Poison:
				{
					effectTarget.TakeDamage(0, 0, 0, damagePerTick, 0);
					break;
				}
				case DOTEffect.DamageType.Electric:
				{
					effectTarget.TakeDamage(0, 0, 0, 0, damagePerTick);
					break;
				}
			}

			yield return new WaitForSeconds(tickDelay);
		}
	}

	IEnumerator Debuff(Enemy effectTarget, float duration, float percent, DebuffEffect.Parameter parameter)
	{
		effectTarget.TakeDebuff(parameter,percent);
		yield return new WaitForSeconds(duration);
		effectTarget.TakeDebuff(parameter,-percent);
		yield break;
	}
}