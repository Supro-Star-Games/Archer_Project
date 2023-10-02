using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectoryRenderer : MonoBehaviour
{
	[SerializeField] private LayerMask _rayMask;
	[SerializeField] private Archer _archer;
	[SerializeField] private float _disabledRadius;
	[SerializeField] private float mouseYOffset = 70f;
	[SerializeField] private GameObject _targetPrefab;

	private LineRenderer _lineRenderer;
	private Vector3 distanceToArcher;
	private Vector3[] points = new Vector3[100];
	private GameObject _uiTarget;
	private bool isPerksApplyed;
	public Vector3 TargetPoint { get; private set; }

	void Start()
	{
		_lineRenderer = GetComponentInChildren<LineRenderer>();
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y + mouseYOffset));
			RaycastHit _hit;
			if (Physics.Raycast(mouseRay, out _hit, 100f, _rayMask))
			{
				TargetPoint = _hit.point;
				if (_uiTarget == null)
				{
					_uiTarget = Instantiate(_targetPrefab, _hit.point, Quaternion.identity);
				}

				_uiTarget.transform.position = _hit.point;
				Vector3 archerPointXZ = new Vector3(_archer.transform.position.x, 0f, _archer.transform.position.z);
				//	distanceToArcher = _hit.point - _archer.transform.position;
				distanceToArcher = _hit.point - archerPointXZ;
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

				if (!isPerksApplyed)
				{
					_archer.RandomizePerks();
					isPerksApplyed = true;
				}
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
			Destroy(_uiTarget.gameObject);
			_uiTarget = null;
			_lineRenderer.enabled = false;
			isPerksApplyed = false;
		}
	}
}