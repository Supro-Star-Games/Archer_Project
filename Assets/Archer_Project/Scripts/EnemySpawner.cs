using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Transform _enemyLineTarget;
	private List<Enemy> _enemies = new List<Enemy>();
	[SerializeField] private List<Transform> _lineTargets;
	[SerializeField] private List<Transform> _spawnPoints;
	[SerializeField] private float _spawnDelay;

	private int currentEnemyCount = 0;

	private List<Enemy> _alivedEnemies = new List<Enemy>();
	public List<int> WaveEnemies { get; set; }
	private int currentMeleeEnemies;
	private int currentRangeEnemies;
	private int waveSpawnedEnemies;

	private int _currentWave;
	private float _timeFromLastSpawn;


	public List<Enemy> Enemies
	{
		get { return _enemies; }
		set { _enemies = value; }
	}

	private void Update()
	{
		if (_currentWave > WaveEnemies.Count - 1)
		{
			return;
		}

		_timeFromLastSpawn += Time.deltaTime;
		if (_timeFromLastSpawn >= _spawnDelay)
		{
			SpawnEnemy(WaveEnemies[_currentWave]);
			_timeFromLastSpawn = 0f;
		}
	}

	private void DecreaseEnemyCount(Enemy enemy)
	{
		if (enemy.IsRangeEnemy)
		{
			currentRangeEnemies--;
		}
		else
		{
			currentMeleeEnemies--;
		}
	}

	public void SpawnEnemy(int lines)
	{
		int availableMeleeSlots = 0;
		int availableRangeSlots = 0;

		if (currentEnemyCount > _enemies.Count - 1)
		{
			return;
		}

		if (currentMeleeEnemies >= 7 && currentRangeEnemies >= 6)
		{
			availableRangeSlots = 0;
			availableMeleeSlots = 0;
		}
		else
		{
			availableMeleeSlots = 7 - currentMeleeEnemies;
			availableRangeSlots = 6 - currentRangeEnemies;
		}


		for (int i = 0; i < lines; i++)
		{
			if (_enemies[currentEnemyCount].IsRangeEnemy)
			{
				if (availableRangeSlots > 0)
				{
					Enemy _newEnemy = Instantiate(_enemies[currentEnemyCount], _spawnPoints[i]);
					_newEnemy.MovePoint = _lineTargets[i];
					currentRangeEnemies++;
					waveSpawnedEnemies++;
					availableRangeSlots--;
					_newEnemy.OnEnemyDeath += DecreaseEnemyCount;
				}
				else
				{
					return;
				}
			}
			else
			{
				if (availableMeleeSlots > 0)
				{
					Enemy _newEnemy = Instantiate(_enemies[currentEnemyCount], _spawnPoints[i]);
					_newEnemy.MovePoint = _lineTargets[i];
					currentMeleeEnemies++;
					waveSpawnedEnemies++;
					availableMeleeSlots--;
					_newEnemy.OnEnemyDeath += DecreaseEnemyCount;
				}
				else
				{
					return;
				}
			}

			currentEnemyCount++;
		}

		if (waveSpawnedEnemies >= lines)
		{
			_currentWave++;
			waveSpawnedEnemies = 0;
		}
		else
		{
			var dif = lines - waveSpawnedEnemies;
			WaveEnemies[_currentWave] -= dif;
		}
	}
}