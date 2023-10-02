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
	
	[Header("AOE Settings")]
	[SerializeField] protected bool aoe;
	[SerializeField] protected float radius;
	[SerializeField] protected float lifeTime;
	[SerializeField] protected GameObject areaObject;
	
	public float ChanceToProke => chanceToProke;
	public bool IsArrowEffect => isArrowEffect;
	public bool AOE => aoe;
	public float Radius => radius;

	public float LifeTime => lifeTime;
	public GameObject AreaObject => areaObject;

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

	public virtual void ReActivateEffects(Enemy _enemy)
	{
		
	}

	public virtual List<PerkEffect> GetEffects()
	{
		return new List<PerkEffect>();
	}
}