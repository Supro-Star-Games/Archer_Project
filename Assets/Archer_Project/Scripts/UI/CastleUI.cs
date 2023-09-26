using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleUI : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	private Fence _fence;

	private void Awake()
	{
		_fence = FindObjectOfType<Fence>();
	}

	private void OnEnable()
	{
		_fence.FenceDamaged += TakeDamage;
	}

	private void OnDisable()
	{
		_fence.FenceDamaged -= TakeDamage;
	}

	// Update is called once per frame
	public void TakeDamage(float _damage)
	{
		_slider.value -= _damage / 100f;
	}
}