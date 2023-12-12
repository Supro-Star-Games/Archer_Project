using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerCurrencies : MonoBehaviour
{
	public static PlayerCurrencies Instance;

	public event Action<int> OnEXPAdded;

	private int _currentEXP;
	private int _currentCoins;

	public void AddEXP(int value)
	{
		if (value <= 0)
		{
			return;
		}
		_currentEXP += value;
		OnEXPAdded?.Invoke(_currentEXP);
	}
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public bool TryBuyForExp(int price)
	{
		if (price > _currentEXP)
		{
			return false;
		}
		else
		{
			_currentEXP -= price;
			return true;
		}
	}

	public bool TryBuyForCoins(int price)
	{
		if (price > _currentCoins)
		{
			return false;
		}
		else
		{
			_currentCoins -= price;
			return true;
		}
	}
}