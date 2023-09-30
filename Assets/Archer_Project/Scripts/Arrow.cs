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

	public float PhDamage { get; set; }
	public float FDamage { get; set; }
	public float PDamage { get; set; }
	public float EDamage { get; set; }
	public float IDamage { get; set; }

	public List<Perk> _perks = new List<Perk>();


	private List<Vector3> _movePoints = new List<Vector3>();


	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (_currentPoint == _movePoints.Count - 1 || isHit)
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
			_currentPoint++;
		}
	}

	public void SetArrow(float physicsDamage, float fireDamage, float iceDamage, float poisonDamage, float electricDamage, float speed)
	{
		PhDamage = physicsDamage;
		FDamage = fireDamage;
		IDamage = iceDamage;
		PDamage = poisonDamage;
		EDamage = electricDamage;
		_velocity = speed;
	}

	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Enemy>(out Enemy _enemy))
		{
			_enemy.TakeDamage(PhDamage);
			foreach (var perk in _perks)
			{
				perk.ActivateEffects(_enemy);
			}

			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<Collider>().enabled = false;
			gameObject.transform.SetParent(_enemy.transform);
			isHit = true;
			_movePoints.Clear();
		}
	}

	public void SetPoints(Vector3[] points)
	{
		for (int i = 0; i < points.Length; i++)
		{
			_movePoints.Add(points[i]);
		}
	}
}