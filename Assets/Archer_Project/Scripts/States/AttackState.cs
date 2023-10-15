using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
	[SerializeField] private float _damage;
	[SerializeField] private float _attackDelay;

	private Rigidbody _rb;
	private Fence _fence;
	private float time;
	private float rotationTime;
	private Animator _animator;


	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_fence = FindObjectOfType<Fence>();
		_animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		_animator.SetBool("IsAttacking", true);
	}

	private void Update()
	{
		Rotate();
		Attack();
	}

	private void Rotate()
	{
		Vector3 fenceDirection = _fence.transform.position - _rb.position;
		Vector3 fenceXZ = new Vector3(fenceDirection.x, 0f, fenceDirection.z);
		if (rotationTime <= 1)
		{
			rotationTime += Time.deltaTime / 2f;
			_rb.rotation = Quaternion.Lerp(_rb.rotation, Quaternion.LookRotation(fenceXZ.normalized), rotationTime);
		}
		else
		{
			_rb.isKinematic = true;
		}
	}

	private void Attack()
	{
		time += Time.deltaTime;
		if (time > _attackDelay)
		{
			_target.TakeDamage(_damage);
			time = 0f;
		}
	}
}