using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Archer : MonoBehaviour
{
	[SerializeField] private Transform _fireTransform;
	[SerializeField] private GameObject _projectile;
	[SerializeField] private float _fireAngle;
	[SerializeField] private float _minAngle = -10f;
	[SerializeField] private float _maxAngle = 15f;
	[SerializeField] private float _maxDistance = 30f;
	[SerializeField] private float _healthPoints = 100f;
	[SerializeField] private float _arrowSpeed = 5f;
	[SerializeField] private float _attackSpeed;

	[Header("Bonus Damage")] [SerializeField]
	private float _physicsDamage;

	[SerializeField] private float _fireDamage;
	[SerializeField] private float _iceDamage;
	[SerializeField] private float _poisonDamage;
	[SerializeField] private float _electricDamage;


	[Header("Bonus Protection")] [SerializeField]
	private float _magickShiled;

	[SerializeField] private float _fireProtection;
	[SerializeField] private float _iceProtection;
	[SerializeField] private float _poisonProtection;
	public event UnityAction<float> ArcherDamaged;

	private float _currentHP;
	private Vector3 _direction;
	private Vector3 _directionXZ;

	[SerializeField] private List<Perk> _learnedActivePerks;
	[SerializeField] private List<Perk> _leranedPassivePerks;
	private List<Perk> _applyedPerks = new List<Perk>();

	public Vector3 ProjectileVelocity { get; set; }

	public Transform FireTransform
	{
		get { return _fireTransform; }
	}

	private void Start()
	{
		ApplyPassivePerks();
	}

	private void Update()
	{
		_fireTransform.localEulerAngles = new Vector3(-_fireAngle, 0f, 0f);
	}

	public void RotateArcher(Vector3 _mousePos)
	{
		_direction = _mousePos - _fireTransform.position;
		_directionXZ = new Vector3(_direction.x, 0, _direction.z);

		transform.rotation = Quaternion.LookRotation(_directionXZ);
	}

	public void RandomizePerks()
	{
		List<Perk> _successedPerks = new List<Perk>();
		foreach (var _perk in _learnedActivePerks)
		{
			if (_perk.IsArrowEffect)
			{
				if (Random.Range(0, 100) <= _perk.ChanceToProke)
				{
					_successedPerks.Add(_perk);
				}
			}
			else
			{
				if (Random.Range(0, 100) <= _perk.ChanceToProke)
				{
					_applyedPerks.Add(_perk);
				}
			}
		}

		if (_successedPerks.Count > 1)
		{
			int randomizedPerk = Random.Range(0, _successedPerks.Count - 1);
			_applyedPerks.Add(_successedPerks[randomizedPerk]);
		}
		else if (_successedPerks.Count == 1)
		{
			_applyedPerks.Add(_successedPerks[0]);
		}
	}

	public void ApplyPassivePerks()
	{
		foreach (var perk in _leranedPassivePerks)
		{
			Debug.Log("Activate");
			perk.PassiveActivate(this);
			perk.DeActivate(this);
		}
	}

	public void RemovePerks()
	{
		foreach (var perk in _applyedPerks)
		{
			perk.DeActivate(this);
		}

		_applyedPerks.Clear();
	}

	public void CalcuateVelocity()
	{
		//	float x = _direction.magnitude;
		float x = _directionXZ.magnitude;
		float y = _direction.y;

		_fireAngle = Mathf.Lerp(_minAngle, _maxAngle, x / _maxDistance);
		float angleToRadians = _fireAngle * Mathf.PI / 180f;

		float v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(angleToRadians) * x) * Mathf.Pow(Mathf.Cos(angleToRadians), 2));
		float v = Mathf.Sqrt(v2);
		ProjectileVelocity = _fireTransform.forward * v;
	}

	public void Shot(Vector3[] _points)
	{
		GameObject newProjectile = Instantiate(_projectile, _fireTransform.position, _fireTransform.rotation);
		Arrow newArrow = newProjectile.GetComponent<Arrow>();
		newArrow._perks.AddRange(_applyedPerks);
		newArrow.SetArrow(_physicsDamage, _fireDamage, _iceDamage, _poisonDamage, _electricDamage, _arrowSpeed);
		newArrow.SetPoints(_points);
		RemovePerks();
	}

	public void TakeDamage(float damage)
	{
		ArcherDamaged?.Invoke(damage / (_healthPoints / 100f));
		_currentHP -= damage;
	}

	public void TakePassivePerk(PassivePerk.BonusStatType _type, float _percent)
	{
		Debug.Log("TakePassive");
		switch (_type)
		{
			case PassivePerk.BonusStatType.HitPoints:
				Debug.Log("TakeHP");
				_healthPoints += (_healthPoints / 100f) * _percent;
				break;
			case PassivePerk.BonusStatType.ArrowSpeed:
				_arrowSpeed += (_arrowSpeed / 100f) * _percent;
				break;
			case PassivePerk.BonusStatType.AttackSpeed:
				_attackSpeed += (_attackSpeed / 100f) * _percent;
				break;
			case PassivePerk.BonusStatType.PhysicsDamage:
				_physicsDamage += (_physicsDamage / 100f) * _percent;
				break;
			case PassivePerk.BonusStatType.FireDamage:
				if (_fireDamage == 0)
				{
					_fireDamage = _percent;
				}

				_fireDamage += _percent;
				break;
			case PassivePerk.BonusStatType.IceDamage:
				if (_iceDamage == 0)
				{
					_iceDamage = _percent;
				}

				_iceDamage += _percent;
				break;
			case PassivePerk.BonusStatType.ElectricDamage:
				if (_electricDamage == 0)
				{
					_electricDamage = _percent;
				}

				_electricDamage += _percent;
				break;
			case PassivePerk.BonusStatType.PoisonDamage:
				if (_poisonDamage == 0)
				{
					_poisonDamage = _percent;
				}

				_poisonDamage += _percent;
				break;
		}
	}
}