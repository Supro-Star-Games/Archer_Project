using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
	[SerializeField] private GameObject _bowString;
	[SerializeField] private GameObject _rightHand;
	[SerializeField] private Transform _startPosition;
	private Archer _archer;


	private void Awake()
	{
		_archer = FindObjectOfType<Archer>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (_archer.StringPulling)
		{
			_bowString.transform.position = _rightHand.transform.position;
		}
		else
		{
			_bowString.transform.position = _startPosition.position;
		}
	}
}