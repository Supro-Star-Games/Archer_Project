using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageFeedItemUI : MonoBehaviour
{
	[SerializeField] private float height;
	[SerializeField] private float duration;
	private TextMeshProUGUI _damageText;
	private float lifeTime;

	private void Awake()
	{
		_damageText = GetComponent<TextMeshProUGUI>();
	}

	public void SetDamage(float _damage)
	{
		_damageText.text = _damage.ToString();
		transform.DOLocalMove(Vector3.up * height, duration);
	}


	void Update()
	{
		lifeTime += Time.deltaTime;
		if (lifeTime > duration)
		{
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		transform.DOComplete();
	}
}