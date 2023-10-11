using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherUI : MonoBehaviour
{
	[SerializeField] private Slider _HPSlider;

	[SerializeField] private Slider _XPSlider;

	private Archer _archer;

	private void Awake()
	{
		_archer = FindObjectOfType<Archer>();
		_XPSlider.value = 0f;
	}

	private void OnEnable()
	{
		_archer.ArcherDamaged += TakeDamage;
		_archer.ArhcerLVLUp += ResetSlider;
		_archer.ExperienceTaked += TakeXP;
	}

	private void OnDisable()
	{
		_archer.ArcherDamaged -= TakeDamage;
		_archer.ArhcerLVLUp -= ResetSlider;
		_archer.ExperienceTaked -= TakeXP;
	}
	
	public void TakeDamage(float _damage)
	{
		_HPSlider.value -= _damage / 100f;
	}

	public void TakeXP(float exp)
	{
		_XPSlider.value += exp / 100f;
	}

	void ResetSlider()
	{
		_XPSlider.value = 0f;
	}
}