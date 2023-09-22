using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
	private bool isBusy = false;
	private Enemy _enemy;

	public bool IsBusy
	{
		get { return isBusy; }
		set { isBusy = value; }
	}

	// Start is called before the first frame update
	private void RevealPoint()
	{
		isBusy = false;
		_enemy.OnEnemyDeath -= RevealPoint;
	}

	private void OnTriggerEnter(Collider other)
	{
		_enemy = other.GetComponent<Enemy>();
		_enemy.OnEnemyDeath += RevealPoint;
	}
}