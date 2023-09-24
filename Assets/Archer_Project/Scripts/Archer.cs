using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
	[SerializeField] private Transform _fireTransform;
	[SerializeField] private GameObject _projectile;
	[SerializeField] private float _fireAngle;
	[SerializeField] private float _minAngle = -10f;
	[SerializeField] private float _maxAngle = 15f;
	[SerializeField] private float _maxDistance = 30f;
	private Vector3 _direction;
	private Vector3 _directionXZ;

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
		_direction = _mousePos - transform.position;
		_directionXZ = new Vector3(_direction.x, 0, _direction.z);

		transform.rotation = Quaternion.LookRotation(_directionXZ);
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
		newProjectile.GetComponent<Arrow>().SetPoints(_points);
	}
}