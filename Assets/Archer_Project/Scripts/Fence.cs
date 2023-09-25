using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
	[SerializeField] private float _durability = 500;
	[SerializeField] private CastleUI _castleUI;
	private float currentDurability;

	// Start is called before the first frame update
	private void Awake()
	{
		currentDurability = _durability;
	}

	public void TakeDamege(float _damage)
	{
		if (currentDurability <= 0)
		{
			return;
		}

		Debug.Log("Drability is" + currentDurability);
		currentDurability -= _damage;
		Debug.Log("castle damage" + _damage / (_durability / 100f));
		_castleUI.TakeDamage(_damage / (_durability / 100f));
	}
}