using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float _velocity = 1f;
	[SerializeField] private int hitPoints;
	[SerializeField] private bool isRangeEnemy;


	private MeleeAttackPoints _points;
	private Transform _movePoint;
	private int currentHP;
	private Rigidbody _rb;
	private bool pointIsReached;
	private bool _attackPositionSeted;
	private Vector3 currentMovePoint;

	public event UnityAction OnEnemyDeath;

	public Transform MovePoint
	{
		set { _movePoint = value; }
		get { return _movePoint; }
	}

	public bool IsRangeEnemy => IsRangeEnemy;

	// Start is called before the first frame update
	void Start()
	{
		if (isRangeEnemy)
		{
			Debug.Log("isRange Enemy");
			_points = GameObject.FindGameObjectWithTag("RangeAttackPoints").GetComponent<RangeAttackPoints>();
		}
		else
		{
			_points = GameObject.FindGameObjectWithTag("MeleeAtackPoints").GetComponent<MeleeAttackPoints>();
		}

		_rb = GetComponent<Rigidbody>();
		currentHP = hitPoints;
		currentMovePoint = _movePoint.position;
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		Move();

		if (currentHP <= 0)
		{
			Death();
		}
	}

	void Move()
	{
		Vector3 direction = currentMovePoint - transform.position;
		_rb.velocity = direction.normalized * _velocity;
		Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
		_rb.rotation = Quaternion.LookRotation(directionXZ);
		float distance = Vector3.Distance(transform.position, currentMovePoint);
		if (distance < 0.4f)
		{
			//_rb.velocity = direction.normalized * distance;
			_rb.velocity = Vector3.zero;
			pointIsReached = true;
			if (_attackPositionSeted)
			{
				_rb.rotation = Quaternion.LookRotation(Vector3.forward);
			}
		}

		if (pointIsReached && !_attackPositionSeted)
		{
			foreach (var _point in _points.AttackPoints)
			{
				if (!_point.IsBusy)
				{
					currentMovePoint = _point.transform.position;
					_point.IsBusy = true;
					_attackPositionSeted = true;
					break;
				}
			}
		}
	}

	public void TakeDamage(int damage)
	{
		currentHP -= damage;
	}

	public void Death()
	{
		OnEnemyDeath?.Invoke();
		Destroy(gameObject);
	}
}