using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	private float _velocity;
	private int _currentPoint;
	private Rigidbody _rb;
	private bool isHit;
	private Enemy _hittedEnemy;

	public float BaseDamage = 20f;

	public List<Perk> _perks = new List<Perk>();


	private List<Vector3> _movePoints = new List<Vector3>();

	private List<float> _damageBonus = new List<float>();


	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		/*
		if (_currentPoint == _movePoints.Count - 1 || isHit)
		{
			return;
		}
		*/
		if (isHit)
		{
			return;
		}

		Vector3 _projectileDirection = _movePoints[_currentPoint] - transform.position;
		if (_projectileDirection != Vector3.zero)
		{
			_rb.rotation = Quaternion.LookRotation(_projectileDirection);
		}

		_rb.velocity = _projectileDirection.normalized * _velocity;
		float distance = Vector3.Distance(_movePoints[_currentPoint], transform.position);
		if (distance < 0.5f)
		{
			//	_currentPoint++;
		}
	}

	public void SetArrow(Dictionary<string, float> _bonuses)
	{
		BaseDamage = _bonuses["phDamage"];
		_damageBonus.Add(_bonuses["fDamage"]);
		_damageBonus.Add(_bonuses["iDamage"]);
		_damageBonus.Add(_bonuses["pDamage"]);
		_damageBonus.Add(_bonuses["eDamage"]);
		foreach (var perk in _perks)
		{
			perk.SetDamageBonus(_damageBonus);
		}

		_velocity = _bonuses["ArrowSpeed"];
	}

	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Enemy>(out Enemy _enemy))
		{
			if (_enemy.IsDead)
			{
				return;
			}

			_hittedEnemy = _enemy;
			_enemy.TakeDamage(BaseDamage);
			foreach (var perk in _perks)
			{
				if (perk.AOE)
				{
					AOESphere _area = Instantiate(perk.AreaObject, _enemy.transform.position, Quaternion.identity).GetComponent<AOESphere>();
					_area.AOEPerk = perk;
				}

				perk.ActivateEffects(_enemy);
			}

			Destroy(gameObject);
			//	SetParent(_enemy.transform);
		}
		else
		{
			foreach (var perk in _perks)
			{
				if (perk.AOE)
				{
					AOESphere _area = Instantiate(perk.AreaObject, other.ClosestPoint(transform.position), Quaternion.identity).GetComponent<AOESphere>();
					_area.AOEPerk = perk;
				}
			}

			SetParent(other.transform);
		}

		_perks.Clear();
		_movePoints.Clear();
	}

	private void SetParent(Transform _target)
	{
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Collider>().enabled = false;
		gameObject.transform.SetParent(_target.transform);
		isHit = true;
	}

	public void SetPoints(Vector3[] points)
	{
		for (int i = 0; i < points.Length; i++)
		{
			_movePoints.Add(points[i]);
		}
	}
}