using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAttack : State
{
	[SerializeField] private float _velocity;

	private Vector3 _attackPoint;
	private Rigidbody _rb;
	private Animator _animator;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		_enemy.GetAttackPoint();
		_attackPoint = _enemy.CurrentPoint;
		_animator.SetBool("IsRunning", true);
	}

	void FixedUpdate()
	{
		Vector3 direction = _attackPoint - transform.position;
		_rb.velocity = direction.normalized * _velocity;
		Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
		_rb.rotation = Quaternion.LookRotation(directionXZ);
	}

	private void OnDisable()
	{
		_animator.SetBool("IsRunning", false);
	}
}