using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Perk : ScriptableObject
{
	[Header("Perk Data")] [SerializeField] private Sprite _perkIcon;
	[SerializeField] private String _perkName;
	[SerializeField] private String _perkDescription;
	[SerializeField] protected bool isArrowEffect;
	[SerializeField] protected float chanceToProke;

	public Sprite PerkIcon => _perkIcon;
	public String PerkName => _perkName;
	public String PerkDescription => _perkDescription;
	
	
	[Header("AOE Settings")]
	[SerializeField] protected bool aoe;
	[SerializeField] protected float radius;
	[SerializeField] protected float lifeTime;
	[SerializeField] protected GameObject areaObject;

	protected List<float> _damageBonuses = new List<float>();

	protected bool isActivated = false;
	public float ChanceToProke => chanceToProke;
	public bool IsArrowEffect => isArrowEffect;
	public bool AOE => aoe;
	public float Radius => radius;

	public float LifeTime => lifeTime;
	public GameObject AreaObject => areaObject;

	public virtual void PassiveActivate(Archer _archer)
	{
		
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

	public virtual void SetDamageBonus(List<float> _bonuses)
	{
		_damageBonuses = _bonuses;
	}
	public virtual List<PerkEffect> GetEffects()
	{
		return new List<PerkEffect>();
	}
}