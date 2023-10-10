using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWave", menuName = "Wave/newWave")]
public class AttackWave : ScriptableObject
{
	[SerializeField] private int _points;
	[SerializeField] private List<Enemy> _enemies;
	[SerializeField] private int _lines;

	public int Points => _points;

	public int Lines => _lines;

	public List<Enemy> Enemies
	{
		get { return _enemies; }
		set { _enemies = value; }
	}
}