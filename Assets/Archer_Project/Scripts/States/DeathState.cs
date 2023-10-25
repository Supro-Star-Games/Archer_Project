using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DeathState : State
{
	[SerializeField] private float timeToDespawn;

	private float time;
	private Rigidbody _rb;
	private Animator _animator;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		_animator.SetBool("IsRunning", false);
		_animator.SetBool("IsAttacking", false);
		_animator.SetBool("IsDying", true);
		_rb.isKinematic = true;
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (time >= timeToDespawn)
		{
			Destroy(gameObject);
		}
	}

	private void OnDisable()
	{
		_animator.SetBool("IsDying", false);
	}
}