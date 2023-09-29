using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedUI : MonoBehaviour
{
	[SerializeField] private GameObject _feedItemPrefab;
	[SerializeField] private Transform _content;
	private Enemy _enemy;


	private void Awake()
	{
		_enemy = GetComponentInParent<Enemy>();
	}

	private void OnEnable()
	{
		_enemy.OnDamage += RenderDamage;
	}

	private void OnDisable()
	{
		_enemy.OnDamage -= RenderDamage;
	}


	public void RenderDamage(float damage)
	{
		var newitem = Instantiate(_feedItemPrefab, _content, false).GetComponent<DamageFeedItemUI>();
		Debug.Log("Damafe is" + damage);
		newitem.SetDamage(damage);
	}
}