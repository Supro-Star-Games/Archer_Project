using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _fpsValue;
	private float time;
	private int fps;

	private void Update()
	{
		fps++;
		time += Time.deltaTime;
		if (time >= 1)
		{
			_fpsValue.text = fps.ToString();
			fps = 0;
			time = 0;
		}
	}
}