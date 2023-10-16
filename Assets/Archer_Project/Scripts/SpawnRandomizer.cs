using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class SpawnRandomizer : MonoBehaviour
{
	public event UnityAction EnemiesDead;

	[SerializeField] private List<AttackWave> _attackWaves;
	[SerializeField] private List<EnemySpawner> _spawners;
	[SerializeField] private float _spawnDelay;
	[SerializeField] private EnemySpawner _spawner;

	private List<List<Enemy>> _enemies = new List<List<Enemy>>();

	public List<List<Enemy>> Enemies => _enemies;
	private float _timeFromLastSpawn;
	private int currentWave;
	private int _enemiesCount;
	private List<int> _waveLines = new List<int>();
	
	


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
					_enemiesCount++;
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
			_waveLines.Add(enemylist.Count);
			int index = 0;
			foreach (var enemy in enemylist)
			{
				_spawner.Enemies.Add(enemy);
				index++;
			}
		}

		_spawner.WaveEnemies = _waveLines;
	}

	public void CheckWinCondition()
	{
		_enemiesCount--;
		if (_enemiesCount == 0)
		{
			EnemiesDead?.Invoke();
		}
	}

	private void Update()
	{
		/*
		if (currentWave > _enemies.Count - 1)
		{
			return;
		}

		_timeFromLastSpawn += Time.deltaTime;
		if (_timeFromLastSpawn >= _spawnDelay)
		{
			int index = 0;
			foreach (var enemy in _enemies[currentWave])
			{
				index++;
			}
			_spawner.SpawnEnemy(index);
			_timeFromLastSpawn = 0f;
			currentWave++;
		}
		*/
	}
}