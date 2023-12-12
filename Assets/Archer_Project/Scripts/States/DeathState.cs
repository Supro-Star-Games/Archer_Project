using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class DeathState : State
{
	[SerializeField] private float timeToDespawn;

	private float time;
	private Rigidbody _rb;
	private Animator _animator;
	private Collider _capule;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_animator = GetComponentInChildren<Animator>();
		_capule = GetComponent<Collider>();
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		_animator.SetBool("IsRunning", false);
		_animator.SetBool("IsAttacking", false);
		_animator.SetBool("IsDying", true);
		_animator.SetFloat("Random", (float)Random.Range(0, 4));
		_rb.isKinematic = true;
		//	_capule.isTrigger = true;
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