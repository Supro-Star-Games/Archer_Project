using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private float _spawnDelay;
	[SerializeField] private List<Enemy> _enemies = new List<Enemy>();
	[SerializeField] private Transform _enemyLineTarget;

	private int currentEnemyCount = 0;

	private float _timeFromLastSpawn;

	// Start is called before the first frame update
	void Start()
	{
		_timeFromLastSpawn = _spawnDelay;
	}

	void Update()
	{
		if (currentEnemyCount == _enemies.Count - 1)
		{
			return;
		}

		_timeFromLastSpawn += Time.deltaTime;
		if (_timeFromLastSpawn >= _spawnDelay)
		{
			Enemy _newEnemy = Instantiate(_enemies[currentEnemyCount], transform.position, transform.rotation);
			_newEnemy.MovePoint = _enemyLineTarget;
			currentEnemyCount++;
			_timeFromLastSpawn = 0;
		}
	}
}