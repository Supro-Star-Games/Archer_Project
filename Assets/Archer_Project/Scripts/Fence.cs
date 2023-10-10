using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fence : MonoBehaviour
{
	[SerializeField] private float _durability = 500;
	private float currentDurability;

	public event UnityAction<float> FenceDamaged;

	public event UnityAction FenceDestroed;

	// Start is called before the first frame update
	private void Awake()
	{
		currentDurability = _durability;
	}

	public void TakeDamege(float _damage)
	{
		if (currentDurability <= 0)
		{
			FenceDestroed?.Invoke();
			return;
		}

		currentDurability -= _damage;
		//_castleUI.TakeDamage(_damage / (_durability / 100f));
		FenceDamaged?.Invoke(_damage / (_durability / 100f));
	}
}