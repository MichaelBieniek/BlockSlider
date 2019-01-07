using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	[SerializeField] GameObject Player;
	Vector3 offset;

	void Start()
	{
		offset = transform.position - Player.transform.position;
	}

	void LateUpdate()
	{
		float newXPosition = Player.transform.position.x;
		float newZPosition = Player.transform.position.z - 3;

		transform.position = new Vector3(newXPosition, transform.position.y, newZPosition);
	}
}
