using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform _movePoint;
	[SerializeField] private float _velocity = 1f;
	[SerializeField] private int hitPoints;

	private int currentHP;
	private Rigidbody _rb;
	private bool pointIsReached;
	private Vector3 currentMovePoint;

	// Start is called before the first frame update
	void Start()
	{
		_rb = GetComponent<Rigidbody>();
		currentMovePoint = _movePoint.position;
		currentHP = hitPoints;
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		Move();

		if (currentHP <= 0)
		{
			Death();
		}
	}

	void Move()
	{
		Vector3 direction = _movePoint.position - transform.position;
		_rb.velocity = direction.normalized * _velocity;
		Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
		_rb.rotation = Quaternion.LookRotation(directionXZ);
		float distance = Vector3.Distance(transform.position, currentMovePoint);
		if (distance < 1f)
		{
			_rb.velocity = direction.normalized * distance;
			pointIsReached = true;
		}
	}

	public void TakeDamage(int damage)
	{
		currentHP -= damage;
	}

	public void Death()
	{
		Destroy(gameObject);
	}
}