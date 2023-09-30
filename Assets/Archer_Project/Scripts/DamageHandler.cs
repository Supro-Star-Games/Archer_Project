using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  DamageHandler : MonoBehaviour

{
    private DamageHandler _damageHandler;
    public void Activate(Enemy effectTarget, float damagePerTick, float tickDelay, int _ticks, PerkDOTEffect.DamageType _type)
    {
        Debug.Log("ActivateCoroutine");
        StartCoroutine(TickDamge(effectTarget, damagePerTick, tickDelay, _ticks, _type));
    }

    IEnumerator TickDamge(Enemy effectTarget, float damagePerTick, float tickDelay, int _ticks, PerkDOTEffect.DamageType _type)
    {
        for (int ticks = 0; ticks < _ticks; ticks++)
        {
            switch (_type)
            {
                case PerkDOTEffect.DamageType.Fire:
                {
                    effectTarget.TakeDamage(0, damagePerTick, 0, 0, 0);
                    break;
                }
                case PerkDOTEffect.DamageType.Ice:
                {
                    effectTarget.TakeDamage(0, 0, damagePerTick, 0, 0);
                    break;
                }
                case PerkDOTEffect.DamageType.Poison:
                {
                    effectTarget.TakeDamage(0, 0, 0, damagePerTick, 0);
                    break;
                }
                case PerkDOTEffect.DamageType.Electric:
                {
                    effectTarget.TakeDamage(0, 0, 0, 0, damagePerTick);
                    break;
                }
            }

            yield return new WaitForSeconds(tickDelay);
        }
    }
}
