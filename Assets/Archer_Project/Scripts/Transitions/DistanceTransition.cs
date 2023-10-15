using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
	[SerializeField] private float _transitionRange;
	[SerializeField] private float _rangeSpread;

	private void Start()
	{
		//инициализация стейта
	}

	private void Update()
	{
		if (Vector3.Distance(transform.position, _enemy.MovePoint.position) < _transitionRange)
		{
			NeedTransit = true;
		}
	}
}