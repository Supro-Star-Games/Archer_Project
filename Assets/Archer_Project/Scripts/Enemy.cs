using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float _velocity = 1f;
	[SerializeField] private int hitPoints;
	[SerializeField] private float damage;
	[SerializeField] private float attackDelay;
	[SerializeField] private bool isRangeEnemy;


	private MeleeAttackPoints _points;
	private Transform _movePoint;
	private int currentHP;
	private Rigidbody _rb;
	private bool pointIsReached;
	private bool _attackPositionSeted;
	private Vector3 currentMovePoint;
	private Fence _fence;
	private bool isOnAttackPoint;
	private float time;
	private Archer _archer;
	private AttackPoint closestPoint;

	public event UnityAction OnEnemyDeath;

	public Transform MovePoint
	{
		set { _movePoint = value; }
		get { return _movePoint; }
	}

	public bool IsOnAttackPoint
	{
		get { return isOnAttackPoint; }
		set { isOnAttackPoint = value; }
	}

	public bool IsRangeEnemy => IsRangeEnemy;

	// Start is called before the first frame update
	void Start()
	{
		if (isRangeEnemy)
		{
			_points = GameObject.FindGameObjectWithTag("RangeAttackPoints").GetComponent<RangeAttackPoints>();
		}
		else
		{
			_points = GameObject.FindGameObjectWithTag("MeleeAtackPoints").GetComponent<MeleeAttackPoints>();
		}

		_archer = FindObjectOfType<Archer>();
		_fence = FindObjectOfType<Fence>();
		_rb = GetComponent<Rigidbody>();
		currentHP = hitPoints;
		currentMovePoint = _movePoint.position;
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		Move();
		Attack();

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
			float closestDistance = 500f;
			foreach (var _point in _points.AttackPoints)
			{
				if (!_point.IsBusy)
				{
					float pointToDistance = Vector3.Distance(transform.position, _point.transform.position);
					if (pointToDistance < closestDistance)
					{
						closestPoint = _point;
						closestDistance = pointToDistance;
					}
					//	_point.IsBusy = true;
					//	break;
				}
			}

			closestPoint.IsBusy = true;
			_attackPositionSeted = true;
			currentMovePoint = closestPoint.transform.position;
		}
	}

	public void Attack()
	{
		if (IsOnAttackPoint)
		{
			time += Time.deltaTime;
			if (isRangeEnemy && time > attackDelay)
			{
				_archer.TakeDamage(damage);
				time = 0f;
			}
			else
			{
				if (time > attackDelay)
				{
					_fence.TakeDamege(damage);
					time = 0f;
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
		Destroy(gameObject, 0.2f);
	}
}