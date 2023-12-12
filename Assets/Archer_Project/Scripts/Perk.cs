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
	[SerializeField] protected int _basePerkCost;
	protected int perkLVL = 1;
    public List<PerkEffect> Effects { get; protected set; }
	public int PerkLVL => perkLVL;
	public Sprite PerkIcon => _perkIcon;
	public String PerkName => _perkName;
	public String PerkDescription => _perkDescription;

	protected int CurrentPrice;


	[Header("AOE Settings")] [SerializeField]
	protected bool aoe;

	[SerializeField] protected float radius;
	[SerializeField] protected float lifeTime;
	[SerializeField] protected GameObject areaObject;

	protected List<float> _damageBonuses = new List<float>();
	public float ChanceToProke => chanceToProke;
	public bool IsArrowEffect => isArrowEffect;
	public bool AOE => aoe;
	public float Radius => radius;
	public float LifeTime => lifeTime;
	public GameObject AreaObject => areaObject;

	public virtual void PassiveActivate(Archer _archer)
	{
	}

	public virtual void StartActivate(Archer _archer)
	{
	}

	public virtual void ActivateEffects(Enemy _enemy)
	{
	}

	public virtual void ReActivateEffects(Enemy _enemy)
	{
	}

	public virtual void SetDamageBonus(List<float> _bonuses)
	{
		_damageBonuses = _bonuses;
	}

	public virtual void ImprovePerk()
	{
	}

	public virtual void SetEffects()
	{
	}
	
}