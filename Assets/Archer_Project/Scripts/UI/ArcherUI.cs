using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArcherUI : MonoBehaviour
{
	[SerializeField] private Slider _HPSlider;
	[SerializeField] private Slider _XPSlider;
	[SerializeField] private GameObject _shotUI;
	[SerializeField] private Image _shotImage;


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
		_archer.PerksIsApplyed += ShowShotUI;
		_archer.ArrowShoted += HideShotUI;
	}

	private void OnDisable()
	{
		_archer.ArcherDamaged -= TakeDamage;
		_archer.ArhcerLVLUp -= ResetSlider;
		_archer.ExperienceTaked -= TakeXP;
		_archer.PerksIsApplyed -= ShowShotUI;
		_archer.ArrowShoted -= HideShotUI;
	}

	private void ShowShotUI(List<Perk> perks)
	{
		_shotUI.SetActive(true);
		_shotImage.color = Color.red;
		_shotImage.DOFillAmount(1, _archer.AttackSpeed);
	}

	private void HideShotUI()
	{
		_shotImage.DOComplete();
		_shotImage.fillAmount = 0f;
		_shotUI.SetActive(false);
	}

	private void Update()
	{
		if (_shotImage.fillAmount == 1)
		{
			_shotImage.color = Color.green;
		}
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