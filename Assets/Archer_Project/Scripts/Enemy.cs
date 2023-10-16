using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

	private SpawnRandomizer _spawner;
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
	private bool isDead;
	private Quaternion _startRotation;
	private float rotationTime;

	public event UnityAction<Enemy> OnEnemyDeath;
	public event UnityAction<float> OnDamage;


	public Transform MovePoint
	{
		set { _movePoint = value; }
		get { return _movePoint; }
	}

	public Vector3 CurrentPoint => currentMovePoint;

	public bool IsOnAttackPoint
	{
		get { return isOnAttackPoint; }
		set { isOnAttackPoint = value; }
	}

	public bool IsRangeEnemy => isRangeEnemy;
	public int SpawnCost => spawnCost;

	// Start is called before the first frame update
	private void Awake()
	{
		_spawner = FindObjectOfType<SpawnRandomizer>();
	}

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

	public void GetAttackPoint()
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
			}
		}

		if (closestPoint == null)
		{
			return;
		}

		closestPoint.IsBusy = true;
		closestPoint.SetEnemy(this);
		_attackPositionSeted = true;
		currentMovePoint = closestPoint.transform.position;
	}

	private void FixedUpdate()
	{
		if (!isDead)
		{
			//	Move();
			//	Attack();
			if (currentHP <= 0)
			{
				Death();
			}
		}
	}

	void Move()
	{
		float distance = Vector3.Distance(transform.position, currentMovePoint);
		if (distance < 0.4f)
		{
			//_rb.velocity = direction.normalized * distance;
			_rb.velocity = Vector3.zero;
			pointIsReached = true;
			if (_attackPositionSeted)
			{
				Debug.Log("_attackposition setted");
				Vector3 fenceDirection = _fence.transform.position - _rb.position;
				Vector3 fenceXZ = new Vector3(fenceDirection.x, 0f, fenceDirection.z);
				//	_rb.rotation = Quaternion.LookRotation(fenceXZ)
				if (rotationTime <= 1)
				{
					rotationTime += Time.deltaTime / 2f;
					// Используйте Quaternion.Slerp для плавного вращения
					_rb.rotation = Quaternion.Lerp(_rb.rotation, Quaternion.LookRotation(fenceXZ.normalized), rotationTime);
				}
				else
				{
					_rb.rotation = Quaternion.LookRotation(fenceXZ);
				}

				;
				//_rb.DOLookAt(_fence.transform.position, 1f);
				return;
			}
		}

		Vector3 direction = currentMovePoint - transform.position;
		_rb.velocity = direction.normalized * _velocity;
		Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
		_rb.rotation = Quaternion.LookRotation(directionXZ);

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

	public void TakeDamage(float _damage, DOTEffect.DamageType _damageType = DOTEffect.DamageType.Physics)
	{
		float _Damage = 0f;
		switch (_damageType)
		{
			case DOTEffect.DamageType.Fire:
			{
				if (fireProtection < _damage)
				{
					_Damage -= _damage - (_damage / 100) * fireProtection;
					currentHP += _Damage;
				}

				break;
			}
			case DOTEffect.DamageType.Ice:
			{
				if (iceProtection < _damage)
				{
					_Damage -= _damage - (_damage / 100) * iceProtection;
					currentHP += _Damage;
				}

				break;
			}
			case DOTEffect.DamageType.Poison:
			{
				if (poisonProtection < _damage)
				{
					_Damage -= _damage - (_damage / 100) * poisonProtection;
					currentHP += _Damage;
				}

				break;
			}
			case DOTEffect.DamageType.Electric:
			{
				if (electricProtection < _damage)
				{
					_Damage -= _damage - (_damage / 100) * electricProtection;
					currentHP += _Damage;
				}

				break;
			}
			case DOTEffect.DamageType.Physics:
			{
				if (physicsProtection < _damage)
				{
					_Damage -= _damage - (_damage / 100) * physicsProtection;
					currentHP += _Damage;
				}

				break;
			}
		}

		OnDamage?.Invoke(Mathf.Abs(_Damage));
	}

	public void TakeDebuff(DebuffEffect.Parameter _parameter, float percent)
	{
		switch (_parameter)
		{
			case DebuffEffect.Parameter.AttackDamage:
				damage -= (damage / 100) * percent;
				break;
			case DebuffEffect.Parameter.AttackSpeed:
				attackDelay += (attackDelay / 100) * percent;
				break;
			case DebuffEffect.Parameter.MoveSpeed:
				_velocity -= (_velocity / 100) * percent;
				break;
			case DebuffEffect.Parameter.PhysicsProtection:
				physicsProtection -= percent;
				break;
		}
	}

	public void Death()
	{
		OnEnemyDeath?.Invoke(this);
		_archer.TakeExperience(expForKill);
		Destroy(gameObject, 0.2f);
		_spawner.CheckWinCondition();
		isDead = true;
	}
}