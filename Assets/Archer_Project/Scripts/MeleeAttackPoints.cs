using UnityEngine;

public class MeleeAttackPoints : MonoBehaviour
{
	private AttackPoint[] _attackPoints;


	private void Awake()
	{
		_attackPoints = GetComponentsInChildren<AttackPoint>();
	}


	public AttackPoint[] AttackPoints => _attackPoints;
}