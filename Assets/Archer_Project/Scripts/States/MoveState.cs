using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveState : State
{
	[SerializeField] private float _velocity;

	private Rigidbody _rb;
	private Vector3 _movePosition;
	private Animator _animator;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		_movePosition = _enemy.CurrentPoint;
		_animator.SetBool("IsRunning",true);
	}

	void FixedUpdate()
	{
		Vector3 direction = _movePosition - transform.position;
		_rb.velocity = direction.normalized * _velocity;
		Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
		_rb.rotation = Quaternion.LookRotation(directionXZ);
	}

	private void OnDisable()
	{
		_animator.SetBool(0,false);
	}
}