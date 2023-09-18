using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectoryRenderer : MonoBehaviour
{
	[SerializeField] private LayerMask _rayMask;
	[SerializeField] private Archer _archer;
	private LineRenderer _lineRenderer;
	Vector3[] points = new Vector3[100];


	public Vector3 MousePoint { get; private set; }

	// Start is called before the first frame update
	void Start()
	{
		_lineRenderer = GetComponentInChildren<LineRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit _hit;

			if (Physics.Raycast(mouseRay, out _hit, 100f, _rayMask))
			{
				MousePoint = _hit.point;
				_archer.RotateArcher(_hit.point);
				_archer.CalcuateVelocity();

				for (int i = 0; i < points.Length; i++)
				{
					float time = i * 0.1f;
					points[i] = _archer.FireTransform.position + _archer.ProjectileVelocity * time + (Physics.gravity * Mathf.Pow(time, 2) / 2f);
				}

				_lineRenderer.enabled = true;
				_lineRenderer.SetPositions(points);
			}
		}


		if (Input.GetMouseButtonUp(0))
		{
			_archer.Shot(points);
			_lineRenderer.enabled = false;
		}
	}
}