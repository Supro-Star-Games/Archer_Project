using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleUI : MonoBehaviour
{
	[SerializeField] private Slider _slider;


	private void Awake()
	{
		//	_slider = GetComponent<Slider>();
	}

	// Update is called once per frame
	public void TakeDamage(float _damage)
	{
		_slider.value -= _damage / 100f;
	}
}