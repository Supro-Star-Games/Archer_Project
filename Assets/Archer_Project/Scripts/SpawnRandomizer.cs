using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnRandomizer : MonoBehaviour
{
	[SerializeField] private List<AttackWave> _attackWaves;
	[SerializeField] private List<EnemySpawner> _spawners;
	[SerializeField] private float _spawnDelay;

	private List<List<Enemy>> _enemies = new List<List<Enemy>>();

	public List<List<Enemy>> Enemies => _enemies;
	private float _timeFromLastSpawn;
	private int currentWave;


	void Start()
	{
		foreach (var _wave in _attackWaves)
		{
			int points = _wave.Points;
			List<Enemy> _newWaveList = new List<Enemy>();
			for (int i = 0; i < _wave.Lines; i++)
			{
				int randomizedEnemy = Random.Range(0, _wave.Enemies.Count - 1);
				if (points >= _wave.Enemies[randomizedEnemy].SpawnCost)
				{
					points -= _wave.Enemies[randomizedEnemy].SpawnCost;
					_newWaveList.Add(_wave.Enemies[randomizedEnemy]);
				}
				else
				{
					break;
				}
			}

			_enemies.Add(_newWaveList);
		}

		foreach (var enemylist in _enemies)
		{
			int index = 0;
			foreach (var enemy in enemylist)
			{
				_spawners[index].Enemies.Add(enemy);
				index++;
			}
		}
	}

	private void Update()
	{
		if (currentWave >= _enemies.Count - 1)
		{
			return;
		}

		_timeFromLastSpawn += Time.deltaTime;
		if (_timeFromLastSpawn >= _spawnDelay)
		{
			int index = 0;
			foreach (var enemy in _enemies[currentWave])
			{
				_spawners[index].SpawnEnemy();
				index++;
			}

			_timeFromLastSpawn = 0f;
			currentWave++;
		}
	}
}