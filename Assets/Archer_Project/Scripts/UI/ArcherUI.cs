using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherUI : MonoBehaviour
{
	[SerializeField] private Slider _slider;

	private Archer _archer;
	
	private void Awake()
	{
		_archer = FindObjectOfType<Archer>();
	}

	private void OnEnable()
	{
		_archer.ArcherDamaged += TakeDamage;
	}

	private void OnDisable()
	{
		_archer.ArcherDamaged -= TakeDamage;
	}

	// Update is called once per frame
	public void TakeDamage(float _damage)
	{
		_slider.value -= _damage / 100f;
	}
}