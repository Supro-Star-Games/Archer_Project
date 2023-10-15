using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
	[SerializeField] private State _firsState;

	private State _currentState;

	public State Current => _currentState;

	private Enemy _enemy;
	private Archer _target;

	private void Awake()
	{
		_enemy = GetComponent<Enemy>();
		_target = FindObjectOfType<Archer>();
	}

	private void Start()
	{
		_enemy = GetComponent<Enemy>();
		if (_enemy == null)
		{
			Debug.Log("EnemyNull");
		}
		_target = FindObjectOfType<Archer>();
		Reset(_firsState);
	}

	private void Update()
	{
		if (_currentState == null)
		{
			return;
		}

		var nextState = _currentState.GetNextState();
		if (nextState != null)
		{
			Transit(nextState);
		}
	}

	private void Reset(State startState)
	{
		_currentState = startState;

		if (_currentState != null)
		{
			_currentState.Enter(_target, _enemy);
		}
	}

	private void Transit(State nextState)
	{
		if (_currentState != null)
			_currentState.Exit();

		_currentState = nextState;

		if (_currentState != null)
		{
			_currentState.Enter(_target, _enemy);
		}
	}
}