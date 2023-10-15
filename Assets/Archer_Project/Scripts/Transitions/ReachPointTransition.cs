using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPointTransition : Transition
{
	void Update()
	{
		if (_enemy.IsOnAttackPoint && Vector3.Distance(_enemy.CurrentPoint, transform.position) < 0.4f)
		{
			NeedTransit = true;
		}
	}
}