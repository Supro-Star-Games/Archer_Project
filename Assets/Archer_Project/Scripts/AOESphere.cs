using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AOESphere : MonoBehaviour
{
	[SerializeField] private float _chekEffectTime = 0.5f;
	public Perk AOEPerk { get; set; }

	private float lifeTime;
	private float _time;
	private float _checkTime;

	private void Start()
	{
		lifeTime = AOEPerk.LifeTime;
		Collider[] _colliders = Physics.OverlapSphere(transform.position, AOEPerk.Radius, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Ignore);
		foreach (var collider in _colliders)
		{
			Enemy _enemy = collider.GetComponent<Enemy>();
			AOEPerk.ActivateEffects(_enemy);
		}
	}

	private void FixedUpdate()
	{
		_checkTime += Time.deltaTime;
		if (_checkTime >= _chekEffectTime)
		{
			Collider[] _colliders = Physics.OverlapSphere(transform.position, AOEPerk.Radius, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Ignore);
			foreach (var collider in _colliders)
			{
				Enemy _enemy = collider.GetComponent<Enemy>();
				AOEPerk.ReActivateEffects(_enemy);
			}

			_checkTime = 0f;
		}
	}

	private void Update()
	{
		_time += Time.deltaTime;
		if (_time >= lifeTime)
		{
			Destroy(gameObject);
		}
	}
}