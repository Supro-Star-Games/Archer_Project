using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
	[Header("Base Сharacteristics")] [SerializeField]
	private float _velocity = 1f;

	[SerializeField] private int hitPoints;
	[SerializeField] private float damage;
	[SerializeField] private float attackDelay;
	[SerializeField] private bool isRangeEnemy;
	[SerializeField] private int spawnCost;
	[SerializeField] private float headDamageMult;
	[SerializeField] private float expForKill;

	[Header("Protective effects")] [SerializeField]
	private float magickShield;

	[SerializeField] private float physicsProtection;
	[SerializeField] private float fireProtection;
	[SerializeField] private float iceProtection;
	[SerializeField] private float poisonProtection;
	[SerializeField] private float electricProtection;

	[Header("Attack effects")] [SerializeField]
	private float physicAttack;

	[SerializeField] private float fireAttack;
	[SerializeField] private float iceAttack;
	[SerializeField] private float poisonAttack;
	[SerializeField] private float electricAttack;


	private MeleeAttackPoints _points;
	private Transform _movePoint;
	private float currentHP;
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
	public event UnityAction<float> OnDamage;

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

	public bool IsRangeEnemy => isRangeEnemy;
	public int SpawnCost => spawnCost;

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

			if (closestPoint == null)
			{
				return;
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

	public void TakeDamage(float physicsDamage, float fireDamage, float iceDamage, float poisonDamage, float electricDamage)
	{
		float _phDamage = 0f;
		float _fDamage = 0f;
		float _iDamage = 0f;
		float _pDamage = 0f;
		float _eDamage = 0f;
		float hpBeforeAttack = currentHP;
		if (physicsProtection < physicsDamage)
		{
			_phDamage = physicsProtection - physicsDamage;
			currentHP += _phDamage;
		}

		if (iceProtection < iceDamage)
		{
			_iDamage = iceProtection - iceDamage;
			currentHP += _iDamage;
		}

		if (fireProtection < fireDamage)
		{
			_fDamage = fireProtection - fireDamage;
			currentHP += _fDamage;
		}

		if (physicsProtection < poisonDamage)
		{
			_pDamage = poisonProtection - poisonDamage;
			currentHP += _pDamage;
		}

		if (electricProtection < electricDamage)
		{
			_eDamage = electricProtection - electricDamage;
			currentHP += _eDamage;
		}

		OnDamage?.Invoke(hpBeforeAttack - currentHP);
	}

	public void Death()
	{
		OnEnemyDeath?.Invoke();
		Destroy(gameObject, 0.2f);
	}
}