using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk", menuName = "Assets/Perks/newPerk")]
public class Perk : ScriptableObject
{
	[Header("Perk Data")] [SerializeField] private Sprite _perkIcon;
	[SerializeField] private String _perkName;
	[SerializeField] private String _perkDescription;
	[SerializeField] protected bool isArrowEffect;
	[SerializeField] protected float chanceToProke;

	public float ChanceToProke => chanceToProke;

	public bool IsArrowEffect => isArrowEffect;

	public virtual bool Activate(Archer _archer)
	{
		return false;
	}

	public virtual void DeActivate(Archer _archer)
	{
	}

	public virtual void ActivateEffects(Enemy _enemy)
	{
		Debug.Log("activateEffects");
	}
}