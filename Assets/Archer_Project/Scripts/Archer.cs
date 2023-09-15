using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
	[SerializeField] private Transform _fireTransform;
	[SerializeField] private GameObject _projectile;
	[SerializeField] private float _fireAngle;
	private Vector3 _direction;
	private Vector3 _directionXZ;

	void Start()
	{
	}


	void Update()
	{
		
	}

	public void RotateArcher(Vector3 _mousePos)
	{
		 _direction = _mousePos - transform.position;
		 _directionXZ = new Vector3(_direction.x, 0, _direction.z);
		
		transform.rotation = Quaternion.LookRotation(_directionXZ);
	}

	public void Shot()
	{
		float x = _directionXZ.magnitude;
		float y = _direction.y;

		float angleToRadians = _fireAngle * Mathf.PI / 180f;

		float v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(angleToRadians) * x) * Mathf.Pow(Mathf.Cos(angleToRadians), 2));
		float v = Mathf.Sqrt(v2);
		GameObject newProjectile = Instantiate(_projectile, _fireTransform.position, _fireTransform.rotation);
		newProjectile.GetComponent<Rigidbody>().velocity = _fireTransform.forward * v;

	}
}