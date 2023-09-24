using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectoryRenderer : MonoBehaviour
{
	[SerializeField] private LayerMask _rayMask;
	[SerializeField] private Archer _archer;
	[SerializeField] private float _disabledRadius;
	private LineRenderer _lineRenderer;
	private Vector3 distanceToArcher;
	Vector3[] points = new Vector3[100];


	public Vector3 TargetPoint { get; private set; }

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
			Ray mouseRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 50f));
			RaycastHit _hit;

			if (Physics.Raycast(mouseRay, out _hit, 100f, _rayMask))
			{
				TargetPoint = _hit.point;
				distanceToArcher = _hit.point - _archer.transform.position;
				if (distanceToArcher.magnitude < _disabledRadius)
				{
					_lineRenderer.enabled = false;
					return;
				}

				_archer.RotateArcher(TargetPoint);
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
			if (distanceToArcher.magnitude < _disabledRadius)
			{
				_lineRenderer.enabled = false;
				return;
			}

			_archer.Shot(points);
			_lineRenderer.enabled = false;
		}
	}
}