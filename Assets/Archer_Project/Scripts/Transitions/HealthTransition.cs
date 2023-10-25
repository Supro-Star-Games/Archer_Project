using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTransition : Transition
{
	private void Start()
	{
		_enemy.OnEnemyDeath += EnemyDeath;
	}

	private void OnDestroy()
	{
		_enemy.OnEnemyDeath -= EnemyDeath;
	}

	public void EnemyDeath(Enemy enemy)
	{
		if (_enemy == enemy)
		{
			NeedTransit = true;
		}
	}
}