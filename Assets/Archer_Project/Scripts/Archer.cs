using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Archer : MonoBehaviour
{
	public event UnityAction<float> ArcherDamaged;
	public event UnityAction ArhcerLVLUp;

	public event UnityAction<float> ExperienceTaked;

	public event UnityAction<List<Perk>> PerksIsApplyed;

	public event UnityAction GameOver;

	public event UnityAction ArrowShoted;

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

	[SerializeField] private List<Perk> _learnedActivePerks;
	[SerializeField] private List<Perk> _leranedPassivePerks;

	private float _currentHP;
	private float _currentXP;
	private float _XPForLevel;
	private float _currentLVL = 1;
	private Vector3 _direction;
	private Vector3 _directionXZ;

	private List<Perk> _applyedPerks = new List<Perk>();


	public Vector3 ProjectileVelocity { get; set; }

	public Transform FireTransform
	{
		get { return _fireTransform; }
	}

	private void Start()
	{
		ApplyPassivePerks();
		_currentHP = _healthPoints;
	}

	private void Update()
	{
		_fireTransform.localEulerAngles = new Vector3(-_fireAngle, 0f, 0f);
		_XPForLevel = 15 * (_currentLVL * 3f);
		if (_currentXP >= _XPForLevel)
		{
			_currentLVL += 1f;
			_currentXP = 0f;
			ArhcerLVLUp?.Invoke();
			GameManager.PauseGame();
		}
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

		PerksIsApplyed?.Invoke(_applyedPerks);
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
		ArrowShoted?.Invoke();
	}

	public void TakeDamage(float damage)
	{
		ArcherDamaged?.Invoke(damage / (_healthPoints / 100f));
		_currentHP -= damage;
		if (_currentHP <= 0)
		{
			GameOver?.Invoke();
		}

		Debug.Log("currentHP is " + _currentHP);
	}

	public void TakeExperience(float _exp)
	{
		_currentXP += _exp;
		float newExp = _exp / (_XPForLevel / 100f);
		ExperienceTaked?.Invoke(newExp);
	}

	public void LearnPerk(Perk perk)
	{
		if (perk is PassivePerk)
		{
			if (_leranedPassivePerks.Contains(perk))
			{
				perk.ImprovePerk();
			}
			else
			{
				_leranedPassivePerks.Add(perk);
			}
		}
		else
		{
			if (_learnedActivePerks.Contains(perk))
			{
				perk.ImprovePerk();
			}
			else
			{
				_learnedActivePerks.Add(perk);
			}
		}

		ApplyPassivePerks();
	}

	public List<Perk> GetLernedPerks()
	{
		List<Perk> _allPerks = new List<Perk>();
		_allPerks.AddRange(_learnedActivePerks);
		_allPerks.AddRange(_leranedPassivePerks);
		return _allPerks;
	}

	public void TakePassivePerk(PassivePerk.BonusStatType _type, float _percent)
	{
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