using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Transform _enemyLineTarget;
	private List<Enemy> _enemies = new List<Enemy>();

	private int currentEnemyCount = 0;


	public List<Enemy> Enemies
	{
		get { return _enemies; }
		set { _enemies = value; }
	}


	public void SpawnEnemy()
	{
		if (currentEnemyCount > _enemies.Count - 1)
		{
			return;
		}

		Enemy _newEnemy = Instantiate(_enemies[currentEnemyCount], transform.position, transform.rotation);
		_newEnemy.MovePoint = _enemyLineTarget;
		currentEnemyCount++;
	}
}