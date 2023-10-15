using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class State : MonoBehaviour
{
	[SerializeField] private List<Transition> _transitions;

	protected Archer _target;

	protected Enemy _enemy;


	public void Enter(Archer target, Enemy enemy)
	{
		if (enabled == false)
		{
			enabled = true;
			_target = target;
			_enemy = enemy;
			foreach (var transition in _transitions)
			{
				transition.enabled = true;
				transition.Init(target, enemy);
			}
		}

		if (_enemy == null)
		{
			Debug.Log("EnemyNull");
		}
	}

	public void Exit()
	{
		if (enabled == true)
		{
			foreach (var transition in _transitions)
			{
				transition.enabled = false;
			}

			enabled = false;
		}
	}

	public State GetNextState()
	{
		foreach (var transition in _transitions)
		{
			if (transition.NeedTransit)
			{
				return transition.TargetState;
			}
		}

		return null;
	}
}