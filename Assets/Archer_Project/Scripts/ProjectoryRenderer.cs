using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectoryRenderer : MonoBehaviour
{
	[SerializeField] private LayerMask _rayMask;
	[SerializeField] private Archer _archer;

	public Vector3 MousePoint { get; private set; }

	// Start is called before the first frame update
	void Start()
	{
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
				Debug.Log("Hit Position" + _hit.point);
				MousePoint = _hit.point;
				_archer.RotateArcher(_hit.point);
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			_archer.Shot();
		}
	}
}