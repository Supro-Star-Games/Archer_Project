using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
	[SerializeField] private List<Stat> _stats;
 
	
	public List<Stat> Stats => _stats;
	
	public enum MenuType
	{
		Attack = 0,
		Defence = 1,
		Economy = 2
	}

	public enum StatType
	{
		HitPoints,
		ArrowSpeed,
		AttackSpeed,
		PhysicsDamage,
		FireDamage,
		IceDamage,
		PoisonDamage,
		ElectricDamage,
		PhysicsProtection,
		FireProtection,
		IceProtection,
		ElectricProtection,
		PoisonProtection
	}

	


	[Serializable]
	public class Stat
	{
		[SerializeField] private string _name;
		[SerializeField] private int _cost;
		[SerializeField] private float _value; 
		[SerializeField] private float _costIncrease;
		[SerializeField] private float _valueIncrease;
		[SerializeField] private MenuType _menuType;
		[SerializeField] private StatType _statType;
		
		public event Action<Stat> OnStatChanged;
		public string Name => _name;
		public int Cost => _cost;
		public float Value => _value;
		public StatType StatType => _statType;
		public MenuType MenuType => _menuType;
		public float CostIncrease => _costIncrease;
		public float ValueIncrease => _valueIncrease;
		
		
		public void IncreaseStat()
		{
			_cost += Mathf.CeilToInt(_cost / 100 * _costIncrease);
			_value += _valueIncrease;
			OnStatChanged?.Invoke(this);
			
		}

		public void UpdateStat(int newCost, float newValue)
		{
			_cost = newCost;
			_value = newValue;
		}
		

	}
}