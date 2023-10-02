using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

	[Header("Bonus Damage")] [SerializeField]
	private float _physicsDamage;

	[SerializeField] private float _fireDamage;
	[SerializeField] private float _iceDamage;
	[SerializeField] private float _poisonDamage;
	[SerializeField] private float _electricDamage;

	public float PhysicsDamage
	{
		get { return _physicsDamage; }
		set { _physicsDamage = value; }
	}

	public float FireDamage
	{
		get { return _fireDamage; }
		set { _fireDamage = value; }
	}

	public float IceDamage
	{
		get { return _iceDamage; }
		set { _iceDamage = value; }
	}

	public float PoisonDamage
	{
		get { return _poisonDamage; }
		set { _poisonDamage = value; }
	}

	public float ElectricDamage
	{
		get { return _electricDamage; }
		set { _electricDamage = value; }
	}

	[Header("Bonus Protection")] [SerializeField]
	private float _magickShiled;

	[SerializeField] private float _fireProtection;
	[SerializeField] private float _iceProtection;
	[SerializeField] private float _poisonProtection;
	public event UnityAction<float> ArcherDamaged;

	private float _currentHP;
	private Vector3 _direction;
	private Vector3 _directionXZ;

	[SerializeField] private List<Perk> _learnedPerks;
	private List<Perk> _applyedPerks = new List<Perk>();

	public Vector3 ProjectileVelocity { get; set; }

	public Transform FireTransform
	{
		get { return _fireTransform; }
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
		foreach (var _perk in _learnedPerks)
		{
			if (_perk.IsArrowEffect)
			{
				if (Random.Range(0, 100) <= _perk.ChanceToProke)
				{
					_successedPerks.Add(_perk);
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
		newArrow.SetArrow(_physicsDamage, _fireDamage, _iceDamage, _poisonDamage, _electricDamage, _arrowSpeed);
		newArrow._perks.AddRange(_applyedPerks);
		newArrow.SetPoints(_points);
		RemovePerks();
	}

	public void TakeDamage(float damage)
	{
		ArcherDamaged?.Invoke(damage / (_healthPoints / 100f));
		_currentHP -= damage;
	}
}